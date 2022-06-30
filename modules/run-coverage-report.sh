REPORTS=""
INPUT_LOCATION=./coverage-input
echo "Cleaning old Coverage Reports"
rm -rf coveragereport
echo "Generating Coverage Reports"
mkdir $INPUT_LOCATION

echo "Testing API..."
dotnet test api --collect:"XPlat Code Coverage" || exit 1
mv ./api/test/TestResults/*/coverage* ./$INPUT_LOCATION
mv ./$INPUT_LOCATION/coverage.cobertura.xml ./$INPUT_LOCATION/api-report.xml
rm -rf ./api/test/TestResults

REPORTS="./$INPUT_LOCATION/api-report.xml"

echo "Testing UI..."
dotnet test end-user-portal --collect:"XPlat Code Coverage" || exit 1
mv ./end-user-portal/gpconnect-user-portal.test/TestResults/*/coverage* ./$INPUT_LOCATION
mv ./$INPUT_LOCATION/coverage.cobertura.xml ./$INPUT_LOCATION/ui-report.xml
rm -rf ./end-user-portal/gpconnect-user-portal.test/TestResults

REPORTS=${REPORTS}";./$INPUT_LOCATION/ui-report.xml"

# echo "Testing Admin Portal..."
# dotnet test end-user-portal --collect:"XPlat Code Coverage"
# mv ./end-user-portal/gpconnect-user-portal.test/TestResults/*/coverage* ./input
# mv ./input/coverage.cobertura.xml ./input/admin-report.xml
# rm -rf ./end-user-portal/gpconnect-user-portal.test/TestResults

# REPORTS=${REPORTS}";./input/admin-report.xml"

echo "Generate HTML Report"
reportgenerator -reports:"${REPORTS}" -targetdir:"coveragereport" -reporttypes:Html -filefilters:"-*.cshtml" -classfilters:"-GpConnect.NationalDataSharingPortal.EndUserPortal.Resources.*;-GpConnect.NationalDataSharingPortal.EndUserPortal.Core.ApplicationBuilderExtensions;-GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.HttpClientExtensions;-GpConnect.NationalDataSharingPortal.EndUserPortal.Core.ServiceCollectionExtensions;-GpConnect.NationalDataSharingPortal.EndUserPortal.Program;-GpConnect.NationalDataSharingPortal.EndUserPortal.Startup;-GpConnect.NationalDataSharingPortal.Api.Program;-GpConnect.NationalDataSharingPortal.Api.Startup;-GpConnect.NationalDataSharingPortal.Api.Core.ApplicationBuilderExtensions"

rm -rf $INPUT_LOCATION && open ./coveragereport/Index.html