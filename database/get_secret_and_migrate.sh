#! /bin/bash

set -eux

SECRET_ARN=$1

read -r FLYWAY_USER FLYWAY_PASSWORD <<< "$(
  aws secretsmanager get-secret-value \
    --secret-id "${SECRET_ARN}" \
    --query 'SecretString' \
    --output text | jq -r '.username,.password'
)"

export FLYWAY_USER
export FLYWAY_PASSWORD

flyway -connectRetries=10 migrate
