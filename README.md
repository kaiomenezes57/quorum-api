# Quorum

Quorum is a poll/voting API with a twist: instead of just voting, each user can also **submit a prediction** for which option they think will win. Once a poll reaches its vote goal, predictions are automatically resolved as **Success** or **Failed** based on the actual winning option — laying the groundwork for a future points/rewards system for users who predict correctly.

> ⚠️ This project is a work in progress. Core domain logic, authentication, and polling/voting/prediction flows are implemented; the points/rewards system and some features are still to come.

## Features

- **Polls** — create polls with multiple options and a configurable vote goal
- **Voting** — cast votes on poll options
- **Predictions** — submit a prediction for which option will win a poll (one active prediction per user per poll); predictions are auto-resolved once the poll's vote goal is reached
- **Authentication** — JWT-based register/login flow
- **Domain-driven design** — business rules (vote goals, prediction resolution, single-active-prediction constraint) enforced inside rich domain entities, not in handlers

## Architecture

Built with **Clean Architecture**, split into four projects:

- `Quorum.Domain` — entities, value objects, and repository interfaces; no external dependencies
- `Quorum.Application` — use cases implemented as CQRS commands/queries via **MediatR**, DTOs, and application interfaces
- `Quorum.Infrastructure` — **EF Core** persistence with **MySQL**, repository implementations, JWT token generation
- `Quorum.API` — ASP.NET Core Web API, controllers, composition root

## Tech stack

- ASP.NET Core (.NET 10)
- Entity Framework Core + MySQL
- MediatR (CQRS)
- JWT authentication
- Docker & Docker Compose
- xUnit (domain unit tests)
- Swagger / OpenAPI

## Running locally

```bash
docker compose up
```

This spins up the API alongside a MySQL container.

## Tests

Domain logic is covered by unit tests in `tests/Quorum.Domain.Tests`, focused on entity behavior (poll creation, options, vote/prediction rules).
