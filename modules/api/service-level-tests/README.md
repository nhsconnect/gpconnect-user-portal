# API - Service Level Tests

These tests use [supertest](https://github.com/visionmedia/supertest) to invoke the API contract. 


## Execute the tests locally 

`./run-tests.sh`

Use docker compose to give a simple one line quickstart

Script will
* Launch a DB
* Setup the current schema
* Launch the API
* Populate the DB with test Data
* Execute the test suite
* * Report back success / fail

## Develop the tests
To help develop the tests it is usually better to run outside of the Docker environment. 

### Install the dependencies
`npm i`

### Setup integration stack
`docker compose up -d api`

### Execute the test suite
`ENDPOINT=<api-url> npm test`

ENDPOINT environment variable sets the API endpoint that you wish to execute the test suite against. 


