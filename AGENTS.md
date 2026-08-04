# AGENTS.md

## Goal
Keep code understandable, maintainable, and Clean Architecture-compliant.

## Priorities
Default conflict-resolution order:
1. Correctness
2. Reliability and operability
3. Maintainability
4. Architectural clarity
5. Performance

## Development phase
- Treat the solution as being in an early design and prototyping phase.
- Breaking changes are acceptable.
- Do not preserve backward compatibility unless explicitly requested.
- Do not create compatibility layers, deprecated APIs, migration shims, or parallel versions of interfaces.
- Prefer improving the current design directly instead of maintaining obsolete behavior.
- Existing code may be renamed, moved, redesigned, or removed when this results in a clearer solution.
- Do not introduce API versioning unless explicitly requested.
- Do not assume that other systems currently depend on this solution.
- Database schemas and migrations may be replaced during development when necessary.
- Prefer simple and explicit implementations over production-oriented complexity.
- After every prompt has been written, append the raw text of the prompt to this file: `docs/prompt-history.md`.
- Do not commit to git unless explicitly asked to do so.

## Technology
- Use ASP.NET Core Web API.
- Use Entity Framework Core with SQLite for local persistence.
- Use the built-in ASP.NET Core dependency injection container.
- Use TUnit for automated tests.
- Use asynchronous APIs for I/O operations.

## Architecture
- `WorldAlerts.Domain` must not depend on other solution projects or infrastructure frameworks.
- `WorldAlerts.Application` may depend on `WorldAlerts.Domain`.
- `WorldAlerts.Infrastructure` may depend on `WorldAlerts.Application` and `WorldAlerts.Domain`.
- `WorldAlerts.Api` may depend on `WorldAlerts.Application` and `WorldAlerts.Infrastructure`.