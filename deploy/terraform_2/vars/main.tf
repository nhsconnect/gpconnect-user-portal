
locals {
  environments = {
    default = local.default,
    # test    = local.test,
  }

  prefix               = "gpc-ndsp"
  env                  = "nonprod"
  region               = "eu-west-2"
  azs                  = ["eu-west-2a", "eu-west-2b"]
  service_account_name = "api-service-account"
}
