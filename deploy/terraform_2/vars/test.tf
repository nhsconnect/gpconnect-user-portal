
locals {
  test = {
    suffix        = "test"
    prefix        = local.prefix
    namespace     = "${local.prefix}-test"
    parent_domain = "ndsp-nonprod.texasplatform.uk"
  }
}
