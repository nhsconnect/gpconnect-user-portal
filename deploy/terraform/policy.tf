data "aws_iam_policy_document" "write_parameter_store" {
  statement {
    actions = [
      "ssm:PutParameter",
      "ssm:DeleteParameters",
      "ssm:DeleteParameter"
    ]
    resources = ["*"]
    effect    = "Deny"
  }
}

data "aws_iam_policy_document" "read_parameter_store" {
  statement {
    actions = [
      "ssm:DescribeParameters",
      "ssm:GetParameters",
      "ssm:GetParameter",
      "ssm:GetParametersByPath",
      "ssm:GetParameterHistory",
    ]
    resources = ["*"]
    effect    = "Allow"
  }
}

resource "aws_iam_policy" "read_parameter_store_policy" {
  path        = "/${module.vars.env.prefix}/"
  name        = "read-parameter-store-policy${module.vars.env.iam_name_suffix}"
  description = "Read parameter store policy"
  policy      = data.aws_iam_policy_document.read_parameter_store.json
}

resource "aws_iam_policy" "write_parameter_store_policy" {
  path        = "/${module.vars.env.prefix}/"
  name        = "write-parameter-store-policy${module.vars.env.iam_name_suffix}"
  description = "Write parameter store policy"
  policy      = data.aws_iam_policy_document.write_parameter_store.json
}

data "aws_iam_policy_document" "read_secrets_manager" {
  statement {
    actions = [
      "secretsmanager:GetResourcePolicy",
      "secretsmanager:GetSecretValue",
      "secretsmanager:DescribeSecret",
      "secretsmanager:ListSecretVersionIds",
      "secretsmanager:ListSecrets",
    ]
    resources = [
      "arn:aws:ecr:eu-west-2:461183108257:secrets/${module.vars.env.prefix}/secrets-manager"
    ]
    effect = "Allow"
  }
}

resource "aws_iam_policy" "read_secrets_manager_policy" {
  path        = "/${module.vars.env.prefix}/"
  name        = "read-secrets-manager-policy${module.vars.env.iam_name_suffix}"
  description = "Read secrets manager policy"
  policy      = data.aws_iam_policy_document.read_secrets_manager.json
}
