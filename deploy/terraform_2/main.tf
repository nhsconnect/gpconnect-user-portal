
terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = "2.12.1"
    }
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.20.1"
    }
  }
}

module "vars" {
  source      = "./vars"
  environment = terraform.workspace
}

data "kubernetes_ingress_v1" "admin" {
  metadata {
    namespace = module.vars.env.namespace
    name      = "alb-admin"
  }
}

locals {
  alb_names = {
    admin = data.kubernetes_ingress_v1.admin.status.0.load_balancer.0.ingress.0.hostname
  }
}

data "aws_lb" "admin" {
  name = join(
    "-",
    slice(
      split("-", local.alb_names["admin"]),
      0, 4
    )
  )
}


data "aws_route53_zone" "default" {
  name = module.vars.env.parent_domain
}

resource "aws_route53_record" "admin" {
  zone_id = data.aws_route53_zone.default.zone_id
  name    = "admin.${module.vars.env.suffix}"
  type    = "A"

  alias {
    name                   = data.aws_lb.admin.dns_name
    zone_id                = data.aws_lb.admin.zone_id
    evaluate_target_health = false
  }
}

output "zone" {
  value = data.aws_route53_zone.default.zone_id
}

output "ingress_name" {
  value = local.alb_names["admin"]
}
