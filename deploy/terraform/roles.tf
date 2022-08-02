
data "aws_caller_identity" "current" {}

data "aws_eks_cluster" "default" {
  name = "live-leks-cluster"
}

data "aws_iam_openid_connect_provider" "default" {
  url = data.aws_eks_cluster.default.identity[0].oidc[0].issuer
}

data "aws_iam_policy_document" "assume_role_eks" {
  for_each = local.db_users
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
      variable = "${data.aws_iam_openid_connect_provider.default.url}:aud"
      values = [
        "sts.amazonaws.com"
      ]
    }
    condition {
      test     = "StringLike"
      variable = "${data.aws_iam_openid_connect_provider.default.url}:sub"
      values = [
        "system:serviceaccount:${module.vars.env.prefix}*:${each.key}-service-account"
      ]
    }
  }
}

resource "aws_iam_role" "api" {
  path = "/${module.vars.env.prefix}/"
  name = "application-api-role${module.vars.env.iam_name_suffix}"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks["api"].json

  tags = {
    Name = "${module.vars.env.prefix}-application-api-role"
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
      "arn:aws:rds-db:eu-west-2:${data.aws_caller_identity.current.account_id}:dbuser:${aws_rds_cluster.default.cluster_resource_id}/${each.key}"
    ]
  }
}

resource "aws_iam_policy" "connect_to_db_as" {
  for_each = local.db_users
  path     = "/${module.vars.env.prefix}/"
  name     = "connect-to-db-as-${each.key}${module.vars.env.iam_name_suffix}"
  policy   = data.aws_iam_policy_document.connect_to_db_as[each.key].json
}

resource "aws_iam_role_policy_attachment" "api_connect_to_db" {
  role       = aws_iam_role.api.name
  policy_arn = aws_iam_policy.connect_to_db_as["api"].arn
}

resource "aws_iam_role" "migrate" {
  path = "/${module.vars.env.prefix}/"
  name = "database-migrate-role${module.vars.env.iam_name_suffix}"

  assume_role_policy = data.aws_iam_policy_document.assume_role_eks["migrate"].json

  tags = {
    Name = "${module.vars.env.prefix}-database-migrate-role"
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
  path   = "/${module.vars.env.prefix}/"
  name   = "read-database-password-secret${module.vars.env.iam_name_suffix}"
  policy = data.aws_iam_policy_document.migrate_db.json
}

resource "aws_iam_role_policy_attachment" "migrator_reads_database_password" {
  role       = aws_iam_role.migrate.name
  policy_arn = aws_iam_policy.read_database_password.arn
}