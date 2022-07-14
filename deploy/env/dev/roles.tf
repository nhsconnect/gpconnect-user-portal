
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
      test = "StringEquals"
      variable = "${local.oidc_provider}:aud"
      values = [
        "sts.amazonaws.com"
      ]
    }
    condition {
      test = "StringEquals"
      variable = "${local.oidc_provider}:sub"
      values = [
        "system:serviceaccount:${local.prefix}:${local.service_account_name}"
      ]
    }
  }
}

resource "aws_iam_role" "migrator" {
  name = "${local.prefix}-database-migration-role"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks.json

  tags = {
    Name = "${local.prefix}-database-migration-role"
  }
}

