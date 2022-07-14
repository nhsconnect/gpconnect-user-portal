
data "aws_caller_identity" "current" {}

# data "aws_eks_cluster" "default" {
#   name = "live-leks-cluster"
# }

locals {
  oidc_issuer = "https://oidc.eks.eu-west-2.amazonaws.com/id/91A8B9E4BB9597FC56C71C89FCAC54D7"
}

# data "aws_iam_openid_connect_provider" "default" {
#   # url = data.aws_eks_cluster.default.identity[0].oidc[0].issuer
#   url = local.oidc_issuer
# }

locals {
  # oidc_provider = data.aws_iam_openid_connect_provider.default.url
  oidc_provider = "oidc.eks.eu-west-2.amazonaws.com/id/91A8B9E4BB9597FC56C71C89FCAC54D7"
  oidc_arn      = "arn:aws:iam::${data.aws_caller_identity.current.account_id}:oidc-provider/${local.oidc_provider}"
}

data "aws_iam_policy_document" "assume_role_eks" {
  for_each = local.db_users
  statement {
    principals {
      type = "Federated"
      identifiers = [
        # data.aws_iam_openid_connect_provider.default.arn
        local.oidc_arn
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
        "system:serviceaccount:${local.prefix}:${each.key}-service-account"
      ]
    }
  }
}

resource "aws_iam_role" "api" {
  path = "/${local.prefix}/"
  name = "application-api-role"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks["api"].json

  tags = {
    Name = "${local.prefix}-application-api-role"
  }
}

locals {
  db_users = toset(["api", "migrate"])
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

resource "aws_iam_role" "migrate" {
  path = "/${local.prefix}/"
  name = "database-migrate-role"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks["migrate"].json

  tags = {
    Name = "${local.prefix}-database-migrate-role"
  }
}

data "aws_iam_policy_document" "migrate_db" {
  statement {
    actions = [
      "secretsmanager:GetSecretValue"
    ]
    resources = [
      aws_secretsmanager_secret.database_password.arn
    ]
  }
}

resource "aws_iam_policy" "read_database_password" {
  path   = "/${local.prefix}/"
  name   = "read-database-password-secret"
  policy = data.aws_iam_policy_document.migrate_db.json
}

resource "aws_iam_role_policy_attachment" "migrator_reads_database_password" {
  role       = aws_iam_role.migrate.name
  policy_arn = aws_iam_policy.read_database_password.arn
}
