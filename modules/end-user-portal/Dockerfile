FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Do the restore first so it's cached as a layer as long as the projects don't change
COPY *.sln ./
COPY gpconnect-user-portal.Helpers/*.csproj gpconnect-user-portal.Helpers/
COPY gpconnect-user-portal.Resources/*.csproj gpconnect-user-portal.Resources/
COPY src/*.csproj src/

RUN dotnet restore

# Now do the real build
COPY . .

RUN dotnet publish -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT dotnet gpconnect-user-portal.dll
