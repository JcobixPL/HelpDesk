# HelpDesk

Backend REST API for a Jira-inspired help desk system built with **.NET 10** and **ASP.NET Core**.

The project was created as a portfolio project to practice building a structured backend application using Clean Architecture, CQRS and modern .NET development practices.

## Tech Stack

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* CQRS + MediatR
* AutoMapper
* FluentValidation
* JWT Authentication & Authorization
* xUnit
* Serilog / structured logging

## Architecture

The solution follows Clean Architecture and is divided into:

`HelpDesk.Api`
HTTP API, controllers and application configuration.

`HelpDesk.Application`
CQRS commands and queries, DTOs, validation and application logic.

`HelpDesk.Domain`
Domain entities, enums and business rules.

`HelpDesk.Infrastructure`
EF Core, PostgreSQL, repositories and external infrastructure.

## Main Features

The application provides functionality for managing projects, tickets, users and comments.

It includes ticket assignment, priorities and statuses, ticket history, authentication and authorization, validation, logging and database persistence.

## Database

PostgreSQL is used as the main database with Entity Framework Core for persistence and migrations.

The project can also be started using Docker Compose.

## Tests

The solution contains automated tests implemented with xUnit.

Run them with:

```bash
dotnet test
```

## Status

The project is in the final stage of development. Core backend functionality and architecture are implemented, with remaining work focused mainly on final improvements and cleanup.
