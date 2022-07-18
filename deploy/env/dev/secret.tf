resource "aws_secretsmanager_secret" "email_configuration" {
  name        = "${local.prefix}-email-configuration"
  description = "Email Configuration"
}

resource "aws_secretsmanager_secret" "fhir_api_query_configuration" {
  name        = "${local.prefix}-fhir-api-query-configuration"
  description = "FHIR API Query Configuration"
}

resource "aws_secretsmanager_secret" "reference_api_query_configuration" {
  name        = "${local.prefix}-reference-api-query-configuration"
  description = "FHIR API Query Configuration"
}

resource "aws_secretsmanager_secret" "spine_configuration" {
  name        = "${local.prefix}-spine-configuration"
  description = "Spine Configuration"
}

resource "aws_secretsmanager_secret" "sso_configuration" {
  name        = "${local.prefix}-sso-configuration"
  description = "Single Sign On Configuration"
}

variable "email-configuration" {
    default = {
        sender_address      = "gpconnect@nhs.net"
        host_name           = "smtp.office365.com"
        port                = 587
        encryption          = "Tls12"
        user_name           = "gpconnect@nhs.net"
        password            = ""
        default_subject     = ""
    }    
    type = map(string)
    sensitive       = true
}

variable "fhir-api-query-configuration" {
    default = {
        get_organisations_with_interactions = "/spine-directory/FHIR/R4/Device?organization=https://fhir.nhs.uk/Id/ods-organization-code|*&identifier=https://fhir.nhs.uk/Id/nhsServiceInteractionId|urn:nhs:names:services:gpconnect*"
        get_organisation_details            = "/STU3/Organization/{odsCode}"
    }
    type = map(string)
}

variable "reference-api-query-configuration" {
    default = {
        get_active_ccg_organisations_from_ods = "/ORD/2-0-0/organisations?Roles=RO98&Limit=1000&Status=Active"
    }    
    type = map(string)
}

variable "spine-configuration" {
    
    default = {
        spine_fhir_api_directory_services_fqdn  = "https://uat.directory.spineservices.nhs.uk"
        spine_fhir_api_systems_register_fqdn    = "https://int.api.service.nhs.uk"
        spine_fhir_api_key                      = "pN2wy8DWGxHyIARkaxNfWLHduEhiVNjc"
    }
    type = map(string)
    sensitive       = true
}

variable "sso-configuration" {
    default = {
        client_id                   = "f1a6b2e9-73ca-451a-9fcb-6a0ac31900ef"
        client_secret               = "HCYYCsDXv2CZa52Dxlf6s9PgnlZCTOGfqyV24eJ3"
        callback_path               = "/auth/externallogin"
        auth_scheme                 = "Cookies"
        challenge_scheme            = "OpenIdConnect"
        auth_endpoint               = "https://fs.nhs.net/adfs/oauth2/authorize/"
        token_endpoint              = "https://fs.nhs.net/adfs/oauth2/token/"
        metadata_endpoint           = "https://fs.nhs.net/adfs/.well-known/openid-configuration"
        signed_out_callback_path    = "/auth/externallogout"
    }
    type = map(string)
    sensitive       = true
}

resource "aws_secretsmanager_secret_version" "email_configuration" {
    secret_id       = aws_secretsmanager_secret.email_configuration.id
    secret_string   = jsonencode(var.email-configuration)
}

resource "aws_secretsmanager_secret_version" "fhir_api_query" {
    secret_id       = aws_secretsmanager_secret.fhir_api_query_configuration.id
    secret_string   = jsonencode(var.fhir-api-query-configuration)
}

resource "aws_secretsmanager_secret_version" "reference_api_query" {
    secret_id       = aws_secretsmanager_secret.reference_api_query_configuration.id
    secret_string   = jsonencode(var.reference-api-query-configuration)
}

resource "aws_secretsmanager_secret_version" "spine_configuration" {
    secret_id       = aws_secretsmanager_secret.spine_configuration.id
    secret_string   = jsonencode(var.spine-configuration)
}

resource "aws_secretsmanager_secret_version" "sso_configuration" {
    secret_id       = aws_secretsmanager_secret.sso_configuration.id
    secret_string   = jsonencode(var.sso-configuration)
}