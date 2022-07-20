#!/bin/bash

# Fail on error
set -e 

# e.g. ./curl_endpoint.sh https://je-webapp-dev-lb.k8s-nonprod.texasplatform.uk/MHSEndpoint 'Hello World!' 5 30

URL=${1?You must pass in the URL of the endpoint to check}
SEARCH_TEXT=${2?You must pass in the text in quotes to search for}
SLEEP_TIME=${3?You must pass in the time to wait in seconds between checks}
MAX_RETRIES=${4?You must pass in the number of times to retry}

RETRY_COUNT=0
while [[ ( $RETRY_COUNT -lt $MAX_RETRIES ) ]]
do
    CURL_OUTPUT=$(curl -s $URL | grep "$SEARCH_TEXT")
    if [[ $CURL_OUTPUT != '' ]]
    then
      echo "CURL_OUTPUT for $URL contains $SEARCH_TEXT, service ready"
      exit 0
    else
      echo "UI not ready"
      ((RETRY_COUNT=RETRY_COUNT + 1))
      echo "RETRIES: $RETRY_COUNT, waiting $SLEEP_TIME seconds"
      sleep $SLEEP_TIME
    fi
done

if [[ $RETRY_COUNT -eq $MAX_RETRIES  ]]
then
  ((TIMEOUT=SLEEP_TIME * MAX_RETRIES))
  echo "UI for $URL didn't start within $TIMEOUT seconds"
  exit 1
fi
