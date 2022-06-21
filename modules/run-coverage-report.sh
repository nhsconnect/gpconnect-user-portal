REPORTS=""
echo "Cleaning old Coverage Reports"
rm -rf coveragereport
echo "Generating Coverage Reports"
mkdir input
echo "Testing API..."
dotnet test api --collect:"XPlat Code Coverage"
mv ./api/test/TestResults/*/coverage* ./input
mv ./input/coverage.cobertura.xml ./input/api-report.xml
rm -rf ./api/test/TestResults
REPORTS="./input/api-report.xml"

echo "Testing API..."
dotnet test end-user-portal --collect:"XPlat Code Coverage"
mv ./end-user-portal/gpconnect-user-portal.test/TestResults/*/coverage* ./ui-report.xml
rm -rf ./end-user-portal/gpconnect-user-portal.test/TestResults
REPORTS=${REPORTS}";./ui-report.xml"

echo "Generate HTML Report"
reportgenerator -reports:"${REPORTS}" -targetdir:"coveragereport" -reporttypes:Html

open ./coveragereport/Index.html