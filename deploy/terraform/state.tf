terraform {
  backend "s3" {
    region               = "eu-west-2"
    bucket               = "nhsd-texasplatform-terraform-service-state-store-lk8s-nonprod"
    workspace_key_prefix = "gpc-ndsp"
    key                  = "gpc-ndsp/nonprod/terraform.tfstate"
    dynamodb_table       = "nhsd-texasplatform-terraform-service-state-lock-texas-lk8s-nonprod"
  }
}
