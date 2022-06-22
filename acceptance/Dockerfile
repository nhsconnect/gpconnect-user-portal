FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /acceptance
COPY *.sln ./
COPY Admin.Specs/*csproj ./Admin.Specs/
COPY User.Specs/*csproj ./User.Specs/
RUN dotnet restore

COPY . .
WORKDIR /acceptance

ENTRYPOINT dotnet test
