
terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.20.1"
    }
    random = {
      source  = "hashicorp/random"
      version = "~> 3.3.2"
    }
  }
}

module "vars" {
  source      = "../vars"
  environment = terraform.workspace
}

data "aws_vpc" "default" {
  filter {
    name   = "tag:Name"
    values = ["lk8s-nonprod.texasplatform.uk"]
  }
}

data "aws_subnet" "private" {
  filter {
    name   = "tag:Name"
    values = ["lk8s-nonprod.texasplatform.uk-private-*"]
  }
  filter {
    name   = "availability-zone"
    values = [module.vars.env.azs[0]]
  }
}

output "database_endpoint" {
  value = aws_rds_cluster_instance.default.endpoint
}

output "database_credentials" {
  sensitive = true
  value = {
    secret_id = aws_secretsmanager_secret.database_password.id
    username  = aws_rds_cluster.default.master_username
  }
}

output "migrate_role_arn" {
  value = aws_iam_role.migrate.arn
}

output "api_role_arn" {
  value = aws_iam_role.api.arn
}

output "cert_arn" {
  value = module.vars.env.cert_arn
}
