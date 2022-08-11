locals {
  default = {
    suffix        = "dev"
    prefix        = local.prefix
    namespace     = "${local.prefix}-dev"
    parent_domain = "ndsp-nonprod.texasplatform.uk"
  }
}
