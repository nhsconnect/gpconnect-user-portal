locals {
  test = {
    iam_name_suffix = "-test"
    prefix          = "${local.prefix}-test"
    azs             = local.azs
    suffix        = "test"
    namespace     = "${local.prefix}-test"
    parent_domain = "ndsp-nonprod.texasplatform.uk"
  }
}
