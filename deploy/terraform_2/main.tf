
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

data "kubernetes_ingress_v1" "application" {
  metadata {
    namespace = module.vars.env.namespace
    name      = "alb-elb"
  }
}

locals {
  alb_names = {
    admin       = data.kubernetes_ingress_v1.admin.status.0.load_balancer.0.ingress.0.hostname
    application = data.kubernetes_ingress_v1.application.status.0.load_balancer.0.ingress.0.hostname
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

data "aws_lb" "application" {
  name = join(
    "-",
    slice(
      split("-", local.alb_names["application"]),
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

resource "aws_route53_record" "application" {
  zone_id = data.aws_route53_zone.default.zone_id
  name    = "transparency.${module.vars.env.suffix}"
  type    = "A"

  alias {
    name                   = data.aws_lb.application.dns_name
    zone_id                = data.aws_lb.application.zone_id
    evaluate_target_health = false
  }
}

resource "aws_route53_record" "application" {
  zone_id = data.aws_route53_zone.default.zone_id
  name    = "apply.${module.vars.env.suffix}"
  type    = "A"

  alias {
    name                   = data.aws_lb.application.dns_name
    zone_id                = data.aws_lb.application.zone_id
    evaluate_target_health = false
  }
}
