resource "random_password" "database_password" {
  length = 20
  special = true
}

resource "aws_ssm_parameter" "database_password_parameter" {
    name        = "${local.prefix}-rds-master-password"
    description = "RDS database password"
    type        = "SecureString"
    value       = random_password.database_password.result
}

variable "source-parameters" {
    type = map
    default = {
        product_name = "National Data Sharing Portal"
        product_name_abbreviated = "NDSP"
        owner_email_address = "gpconnect@nhs.net"
        owner_telephone = "020 3829 0748"
        product_version = "1.0.0" //NEED TO REPLACE THIS WITH A CALCULATED VERSION NUMBER
    }
}

resource "aws_ssm_parameter" "parameters" {
    for_each    = var.source-parameters
    name        = each.key
    type        = "String"
    value       = each.value
}