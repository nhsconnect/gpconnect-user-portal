#!/bin/bash

# Fail on error
set -e 

# e.g. ./check_alb.sh arn:aws:elasticloadbalancing:eu-west-2:730319765130:targetgroup/k8s-jewebapp-jewebapp-3692dc2fa0/faa9876d915d463f 5 30

TG_ARN=${1?You must pass in the Target Group ARN}
SLEEP_TIME=${2?You must pass in the time to wait in seconds between checks}
MAX_RETRIES=${3?You must pass in the number of times to retry}

RETRY_COUNT=0
while [[ ( $RETRY_COUNT -lt $MAX_RETRIES ) ]]
do
    AWSCLI_OUTPUT=$(aws elbv2 describe-target-health --target-group-arn=$TG_ARN --region='eu-west-2' | jq -r '.TargetHealthDescriptions[] | .TargetHealth.State' | uniq)
    if [[ $AWSCLI_OUTPUT == 'healthy' ]]
    then
      echo "All ALB targets in service"
      exit 0
    else
      echo "ALB targets not all in service"
      ((RETRY_COUNT=RETRY_COUNT + 1))
      echo "RETRIES: $RETRY_COUNT, waiting $SLEEP_TIME seconds"
      sleep $SLEEP_TIME
    fi
done

if [[ $RETRY_COUNT -eq $MAX_RETRIES  ]]
then
  ((TIMEOUT=SLEEP_TIME * MAX_RETRIES))
  echo "Targets for $TG_ARN not all in service within $TIMEOUT seconds"
  exit 1
fi
