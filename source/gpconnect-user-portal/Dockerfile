#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["gpconnect-user-portal/gpconnect-user-portal.csproj", "gpconnect-user-portal/"]
RUN dotnet restore "gpconnect-user-portal/gpconnect-user-portal.csproj"
COPY . .
WORKDIR "/src/gpconnect-user-portal"
RUN dotnet build "gpconnect-user-portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gpconnect-user-portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gpconnect-user-portal.dll"]