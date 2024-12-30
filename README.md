# Movie catalog API

## Description
API to manage a movie catalog. The application allows clients to retrieve a list of movies and detailed information about individual movies through RESTful APIs.
Made as a test technical assignment.

## Table of Contents
- [Requirements](#requirements)
- [Installation and building](#installation-and-building)
- [Building](#building)
- [Testing](#testing)
- [Running](#running)
- [Health check](#health-check)
- [Seeding database](#seeding-database)
- [Demo](#demo)

## Requirements
In order to run this solution you will need the following .Net 8.0 SDK installed on our system [more information here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Installation and building
1. Clone the repository
```bash
git clone https://github.com/NodCaleb/MovieCatalog.git
```

2. Navigate to solution directory and run build command
```bash
dotnet build
```

## Testing
Solution contains tests for MediatR queries and commands handlers.

In order to run tests navigate to solution directory and run test command:
```bash
dotnet test
```

## Running 
Navigate to solution directory and run appropriate command:
```bash
dotnet run --project ./MovieCatalog.AppHost/MovieCatalog.AppHost.csproj
```
This will run Aspire app host containing Movie catalog Web API and additional containers required.

## Health check
Use this endpoint to check if Web API is healthy:
```bash
GET /health
```

## Seeding database
Upon starting application all database migrations will be applied automatically.

Alternatively you can apply migrations explicitly by running appropriate command:
```bash
dotnet ef database update
```
On the first run after updating database schema if there is no movies in the catalog database will be seed with test data.

Test data is contained in the following file:
`MovieCatalog.Infrastructure\Data\Seed\Movies.json`

It could be updated if needed, but data structure should stay the same, in case of currupted data, database won't be seeded and exception will be thorown.

## Demo

For demo purposes app has been deployed to Azure:

https://moviecatalog-api.proudmeadow-3791c9e4.northeurope.azurecontainerapps.io/swagger

Feel fre to test in in any way.