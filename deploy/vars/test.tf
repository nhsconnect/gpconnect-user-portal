locals {
  test = {
    iam_name_suffix = "-test"
    prefix          = "${local.prefix}-test"
    azs             = local.azs
    suffix          = "test"
    namespace       = "${local.prefix}-test"
    parent_domain   = "ndsp-nonprod.texasplatform.uk"
    cert_arn        = "arn:aws:acm:eu-west-2:730319765130:certificate/b638152e-e224-40c3-b9cd-c3b8fa70c2ba"
  }
}
