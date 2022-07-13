#!/usr/bin/env bash

# Fail on error
set -e 

ENV=${1?You must pass in the env you are deploying to i.e live-lk8s-nonprod or live-lk8s-prod}

if [[ ${ENV} == 'live-lk8s-nonprod' ]]; then
  BUCKET="nhsd-texasplatform-kubeconfig-lk8s-nonprod"
  FILE="live-leks-cluster_kubeconfig"
elif [[ ${ENV} == 'live-lk8s-prod' ]]; then
  BUCKET="nhsd-texasplatform-kubeconfig-lk8s-prod"
  FILE="live-leks-cluster_kubeconfig"
else
  echo "Invalid env name ${ENV}"
  exit 1
fi

aws s3 cp s3://${BUCKET}/${FILE} ${WORKSPACE}/${FILE} --source-region=eu-west-2 --region=eu-west-2 2>&1 >/dev/null

echo "${WORKSPACE}/${FILE}"
