# API - Service Level Tests

## Install the required dependencies 
npm i

## Execute the tests
ENDPOINT=<api-url> npm test

Currently the test suite pulls the url of the API from the ENDPOINT environment variable. 

NOTE: It is envisaged that a docker-compose stack could be used to create all of required infra for the api and run as a distinct service.

