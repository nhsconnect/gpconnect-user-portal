FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG BUILD_TAG
ENV BUILD_TAG=$BUILD_TAG
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY gpconnect-user-portal.api.sln ./
COPY src/*.csproj ./src/
COPY test/*.csproj ./test/

RUN dotnet restore
COPY . .
WORKDIR /src
RUN dotnet build -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "gpconnect-user-portal.api.dll"]