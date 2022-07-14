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
  name        = "${local.prefix}-read-parameter-store-policy"
  description = "Read parameter store policy"
  policy      = data.aws_iam_policy_document.read_parameter_store.json
}

resource "aws_iam_policy" "write_parameter_store_policy" {
  name        = "${local.prefix}-write-parameter-store-policy"
  description = "Write parameter store policy"
  policy      = data.aws_iam_policy_document.write_parameter_store.json
}
