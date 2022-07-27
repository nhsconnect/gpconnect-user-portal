locals {
  test = {
    iam_name_suffix = "-test"
    prefix          = "${local.prefix}-test"
    azs             = local.azs
  }
}
