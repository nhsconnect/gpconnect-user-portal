#! /bin/bash

set -eux

SECRET_ARN=$1

read -r FLYWAY_USER FLYWAY_PASSWORD <<< "$(
  aws secretsmanager get-secret-value \
    --secret-id "${SECRET_ARN}" \
    --query 'SecretString' \
<<<<<<< HEAD
    --output text | jq -r '[.username, .password] | @tsv'
=======
    --output text | jq -r '.username,.password'
>>>>>>> 31c88dc (Fetch secret from database)
)"

export FLYWAY_USER
export FLYWAY_PASSWORD

flyway -connectRetries=10 migrate
