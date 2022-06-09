<img src="documentation/images/logo.png" height=72>

# GP Connect National Data Sharing Portal UI

The GP Connect National Data Sharing End User Portal is a web application which gives organisations the ability to sign up to the National Data Sharing Agreement so they can use GP Connect products. It also allows users to search for sites using GP Connect and see which services are enabled, eg Access Record HTML.

## Tech stack

- C# / .NET 6.0
- ASP.NET
- Docker
- NHS UK frontend
- Razor

## Repository

This is the source repository for the GP Connect National Data Sharing End User Portal solution.

The current version is held in the master branch, with WIP versions held in task/* branches which are then merged into the develop branch via a pull request.

## Building the solution

To build the End User Portal locally

- Clone the repository: `git clone https://github.com/nhsconnect/gpconnect-user-portal.git`
- Install [Postgres](https://www.postgresql.org/download/)
- Install the DB schema from the database/schema folder
- Install the DB functions from the database/functions folder
- Create environment variable for the database connection
  - Variable name: ConnectionStrings:DefaultConnection
  - Variable value: Server=localhost;Port=5432;Database=GpConnectEndUserPortal;User Id=***USER_NAME_HERE***;Password=***PASSWORD_HERE***;
- Run the solution
- Browse website [Browser](https://localhost:5000): `https://localhost:5000`

## Build status

[![Build Actions Status](https://github.com/nhsconnect/gpconnect-user-portal/workflows/CI/badge.svg)](https://github.com/nhsconnect/gpconnect-user-portal/actions)