locals {
  default = {
    iam_name_suffix = ""
    prefix          = local.prefix
    azs             = local.azs
    suffix        = "dev"
    namespace     = "${local.prefix}-dev"
    parent_domain = "ndsp-nonprod.texasplatform.uk"
  }
}
