#!/usr/bin/env bash

REPO_NAME=${1?You must pass the repo name}
IMAGE_TAG=${2?You must pass in the name of the image tag you have tagged your docker image with}
FAIL_ON_CRITICAL=${3?You must pass in true or false}
REGISTRY_ID='461183108257'

aws ecr get-login-password --region eu-west-2 | docker login --username AWS --password-stdin ${REGISTRY_ID}.dkr.ecr.eu-west-2.amazonaws.com
echo "starting scan"
start_scan="$(aws ecr start-image-scan --registry-id "${REGISTRY_ID}" --repository-name "${REPO_NAME}" --image-id imageTag="${IMAGE_TAG}" --region eu-west-2)"
echo "waiting for scan to finish"
$(aws ecr wait image-scan-complete --registry-id ${REGISTRY_ID} --repository-name ${REPO_NAME} --image-id imageTag=${IMAGE_TAG} --region eu-west-2)
echo "getting  results"
$(aws ecr describe-image-scan-findings --registry-id ${REGISTRY_ID} --repository-name ${REPO_NAME} --image-id imageTag=${IMAGE_TAG} --region eu-west-2 >scan_results.json)

if $FAIL_ON_CRITICAL ; then
  if grep -q '"severity": "CRITICAL"' scan_results.json; then
    echo "CRITICAL Issues found"
    exit 1
  else
    echo "No CRITICAL Issues found"
  fi
else
  echo "FAIL_ON_CRITICAL is false, hence skipping verification"
fi
