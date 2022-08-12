locals {
  default = {
    iam_name_suffix = ""
    prefix          = local.prefix
    azs             = local.azs
    suffix          = "dev"
    namespace       = "${local.prefix}-dev"
    parent_domain   = "ndsp-nonprod.texasplatform.uk"
    cert_arn        = "arn:aws:acm:eu-west-2:730319765130:certificate/b638152e-e224-40c3-b9cd-c3b8fa70c2ba"
  }
}
