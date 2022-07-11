
terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.20.1"
    }
  }
}


locals {
  prefix = "gpc-ndsp"
  region = "eu-west-2"
  azs    = ["eu-west-2a", "eu-west-2b"]
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
    values = [ local.azs[0] ]
  }
}
