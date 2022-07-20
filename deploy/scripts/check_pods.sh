#!/bin/bash

# Fail on error
set -e

# e.g. ./check_pods.sh je-webapp je-webapp-dev 5 30

POD_PREFIX=${1?You must pass in the pod name prefix}
NAMESPACE=${2?You must pass in the namespace}
SLEEP_TIME=${3?You must pass in the time to wait in seconds between checks}
MAX_RETRIES=${4?You must pass in the number of times to retry}

RETRY_COUNT=0
while [[ ( $RETRY_COUNT -lt $MAX_RETRIES ) ]]
do
    NUM_PODS_NOT_RUNNING=$(kubectl get pods -n $NAMESPACE | grep "^$POD_PREFIX" | grep -v 'Running' | wc -l)
    # e.g. 3/3 pods ready will evaluate to 1 mathematically with bc
    PODS_READY_PROPORTION=$(kubectl get pods -n $NAMESPACE | grep "^$POD_PREFIX" | awk '{print $2}' | bc)
    if [[ $NUM_PODS_NOT_RUNNING -eq 0 && $PODS_READY_PROPORTION -eq 1 ]]
    then
      echo "Pods running and ready"
      exit 0
    else
      echo "Not all pods are running and ready yet, waiting ${SLEEP_TIME}s..."
      ((RETRY_COUNT=RETRY_COUNT + 1))
      echo "RETRIES: $RETRY_COUNT, waiting $SLEEP_TIME seconds"
      sleep $SLEEP_TIME
    fi
done

if [[ $RETRY_COUNT -eq $MAX_RETRIES  ]]
then
  ((TIMEOUT=SLEEP_TIME * MAX_RETRIES))
  echo "Pods beginning $POD_PREFIX not ready within $TIMEOUT seconds"
  exit 1
fi
