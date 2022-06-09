FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Do the restore first so it's cached as a layer as long as the projects don't change
COPY src/*.csproj src/

WORKDIR /src
RUN dotnet restore

# Now do the real build
COPY src/ .

RUN dotnet publish -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT dotnet gpconnect-user-portal.dll
