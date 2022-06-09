echo "Testing in ${PWD}"
echo "Clearing existing stack"
docker compose down
echo "Building new stack"
docker compose up -d --build api
echo "Building new stack - completed"
echo "Loading Data"
psql -h localhost -U postgres -w < ./db_data_source/test_data_source.sql
echo "Building Tests"
docker compose build test
echo "Executing Tests"
docker compose up test --exit-code-from test   
RESULT=$?
echo "Tests Completed with exit ${RESULT}"
exit $RESULT;