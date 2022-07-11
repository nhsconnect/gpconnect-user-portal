
terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.20.1"
    }
  }
}


data "aws_vpc" "default" {
  filter {
    name   = "tag:Name"
    values = ["lk8s-nonprod.texasplatform.uk"]
  }
}


output "information" {
  value = data.aws_vpc.default.id
}

