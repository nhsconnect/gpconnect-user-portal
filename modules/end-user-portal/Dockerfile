FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Do the restore first so it's cached as a layer as long as the projects don't change
WORKDIR /src

COPY gpconnect-user-portal.test/*.csproj gpconnect-user-portal.test/
COPY src/*.csproj ./src/
COPY *.sln ./

RUN dotnet restore

# Now do the real build
COPY . .

RUN dotnet publish src/gpconnect-user-portal.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT dotnet gpconnect-user-portal.dll
