#!/bin/bash

# Fail on error
set -e 

# Note the trailing '.'s after the R53 record and target
# e.g. ./check_r53.sh /hostedzone/Z2XTY6OZEUPIQ2 je-webapp-dev-lb.k8s-nonprod.texasplatform.uk. internal-k8s-jewebapp-lb-d84af442d3-1105057367.eu-west-2.elb.amazonaws.com.

ZONE=${1?You must pass in the R53 hosted zone to check}
RECORD=${2?You must pass in the R53 record to check for}
TARGET=${3?You must pass in the R53 record target to check for}
SLEEP_TIME=${4?You must pass in the time to wait in seconds between checks}
MAX_RETRIES=${5?You must pass in the number of times to retry}

RETRY_COUNT=0
while [[ ( $RETRY_COUNT -lt $MAX_RETRIES ) ]]
do
    AWSCLI_OUTPUT=$(aws route53 list-resource-record-sets --hosted-zone-id=$ZONE | jq -r --arg jq_record $RECORD '.ResourceRecordSets[] | select(.Name == $jq_record) | select(.Type == "A") | .AliasTarget.DNSName' )
    echo "DEBUG: AWSCLI_OUTPUT is: $AWSCLI_OUTPUT"
    echo "DEBUG: TARGET is: $TARGET"
    if [[ $AWSCLI_OUTPUT == $TARGET ]]
    then
      echo "R53 record $RECORD correct"
      exit 0
    else
      echo "R53 record $RECORD for $TARGET not yet ready"
      ((RETRY_COUNT=RETRY_COUNT + 1))
      echo "RETRIES: $RETRY_COUNT, waiting $SLEEP_TIME seconds"
      sleep $SLEEP_TIME
    fi
done

if [[ $RETRY_COUNT -eq $MAX_RETRIES  ]]
then
  ((TIMEOUT=SLEEP_TIME * MAX_RETRIES))
  echo "R53 record $RECORD not ready within $TIMEOUT seconds"
  exit 1
fi
