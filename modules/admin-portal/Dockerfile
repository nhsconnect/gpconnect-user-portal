FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Do the restore first so it's cached as a layer as long as the projects don't change
WORKDIR /src

COPY test/*.csproj test/
COPY src/*.csproj src/
COPY *.sln ./

RUN dotnet restore
COPY . .
RUN dotnet build -c Debug -o /app/build

FROM build AS publish
WORKDIR /src/src
RUN dotnet publish -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "gpconnect.nds.admin.portal.dll"]

