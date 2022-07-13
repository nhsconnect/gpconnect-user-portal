#!/bin/bash

# Fail on error
set -e 

# e.g. ./check_ingress.sh lb je-webapp-dev 1633949220 5 30

INGRESS=${1?You must pass in the ingress name}
NAMESPACE=${2?You must pass in the namespace}
EPOCHTIME=${3?You must pass in the epochtime prior to kubectl apply to match against}
SLEEP_TIME=${4?You must pass in the time to wait in seconds between checks}
MAX_RETRIES=${5?You must pass in the number of times to retry}

RETRY_COUNT=0
while [[ ( $RETRY_COUNT -lt $MAX_RETRIES ) ]]
do
    INGRESS_STATE=$(kubectl get ingress $INGRESS -n $NAMESPACE -o json | jq -r '.status.loadBalancer.ingress[].hostname')
    INGRESS_EPOCHTIME=$(kubectl get ingress $INGRESS -n $NAMESPACE -o json | jq -r '.metadata.annotations."last-updated-epochtime"')
    if [[ $INGRESS_EPOCHTIME == $EPOCHTIME && $INGRESS_STATE != '' ]]
    then
      echo "Ingress ready"
      exit 0
    else
      echo "Ingress not ready"
      ((RETRY_COUNT=RETRY_COUNT + 1))
      echo "RETRIES: $RETRY_COUNT, waiting $SLEEP_TIME seconds"
      sleep $SLEEP_TIME
    fi
done

if [[ $RETRY_COUNT -eq $MAX_RETRIES  ]]
then
  ((TIMEOUT=SLEEP_TIME * MAX_RETRIES))
  echo "Ingress $INGRESS not ready within $TIMEOUT seconds"
  exit 1
fi
