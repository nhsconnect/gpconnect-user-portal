FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build



# Do the restore first so it's cached as a layer as long as the projects don't change
COPY *.sln ./
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

# Now do the real build
COPY . .

RUN dotnet publish -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT dotnet gpconnect-user-portal.dll
