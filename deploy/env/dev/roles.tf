
data "aws_caller_identity" "current" {}

data "aws_eks_cluster" "default" {
  name = "live-leks-cluster"
}

data "aws_iam_openid_connect_provider" "default" {
  url = data.aws_eks_cluster.default.identity[0].oidc[0].issuer
}

locals {
  oidc_provider = trimprefix(data.aws_iam_openid_connect_provider.default.url, "https://")
}

data "aws_iam_policy_document" "assume_role_eks" {
  statement {
    principals {
      type = "Federated"
      identifiers = [
        data.aws_iam_openid_connect_provider.default.arn
      ]
    }
    actions = [
      "sts:AssumeRoleWithWebIdentity"
    ]
    condition {
      test     = "StringEquals"
      variable = "${local.oidc_provider}:aud"
      values = [
        "sts.amazonaws.com"
      ]
    }
    condition {
      test     = "StringEquals"
      variable = "${local.oidc_provider}:sub"
      values = [
        "system:serviceaccount:${local.prefix}:${local.service_account_name}"
      ]
    }
  }
}

resource "aws_iam_role" "api" {
  path = "/${local.prefix}/"
  name = "application-api-role"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks.json

  tags = {
    Name = "${local.prefix}-database-migration-role"
  }
}

locals {
  db_users = toset(["api"])
}

data "aws_iam_policy_document" "connect_to_db_as" {
  for_each = local.db_users
  statement {
    actions = [
      "rds-db:connect"
    ]
    resources = [
      "arn:aws:rds-db:${data.aws_caller_identity.current.account_id}:dbuser:${aws_rds_cluster_instance.default.dbi_resource_id}/${each.key}"
    ]
  }
}

resource "aws_iam_policy" "connect_to_db_as" {
  for_each = local.db_users
  path     = "/${local.prefix}/"
  name     = "connect-to-db-as-${each.key}"
  policy   = data.aws_iam_policy_document.connect_to_db_as[each.key].json
}

resource "aws_iam_role_policy_attachment" "api_connect_to_db" {
  role       = aws_iam_role.api.name
  policy_arn = aws_iam_policy.connect_to_db_as["api"].arn
}
