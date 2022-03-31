FROM postgres:11.9
RUN apt-get update && apt-get -y install postgresql-11-cron

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG BUILD_TAG
ENV BUILD_TAG=$BUILD_TAG
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY gpconnect-user-portal.sln ./
COPY gpconnect-user-portal.Admin/*.csproj ./gpconnect-user-portal.Admin/
COPY gpconnect-user-portal.Console/*.csproj ./gpconnect-user-portal.Console/
COPY gpconnect-user-portal.Core/*.csproj ./gpconnect-user-portal.Core/
COPY gpconnect-user-portal.DAL/*.csproj ./gpconnect-user-portal.DAL/
COPY gpconnect-user-portal.Download/*.csproj ./gpconnect-user-portal.Download/
COPY gpconnect-user-portal.DTO/*.csproj ./gpconnect-user-portal.DTO/
COPY gpconnect-user-portal.Functions/*.csproj ./gpconnect-user-portal.Functions/
COPY gpconnect-user-portal.Helpers/*.csproj ./gpconnect-user-portal.Helpers/
COPY gpconnect-user-portal.Resources/*.csproj ./gpconnect-user-portal.Resources/
COPY gpconnect-user-portal.Services/*.csproj ./gpconnect-user-portal.Services/
COPY gpconnect-user-portal/*.csproj ./gpconnect-user-portal/

RUN dotnet restore
COPY . .
WORKDIR /src
RUN dotnet build -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN sed -i "s|DEFAULT@SECLEVEL=2|DEFAULT@SECLEVEL=1|g" /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "gpconnect-user-portal.dll"]