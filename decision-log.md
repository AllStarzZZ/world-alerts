## DEC-001: Handle duplicate events in the application layer

Context

WorldEvent contains an ExternalId that identifies an event in the external source. Duplicate events should not be processed multiple times.

Decision

The database will not enforce a unique constraint on WorldEvent.ExternalId.

Duplicate detection will instead be handled by an application service before a new event is stored.

## DEC-002: Defer validation for WorldEvent

Context

The MVP has a limited implementation timeframe. Adding complete domain validation for WorldEvent would require additional design, error handling, and test coverage.

Decision

No dedicated validation will be implemented for WorldEvent in the current MVP.

Reason

The available time will be prioritized for the core alert-matching and notification workflow.

## DEC-003: No transaction management for notification dispatch and persistence

Context

The NotificationEvaluatorService both dispatches notifications and persists delivery records. Implementing full ACID transaction semantics with rollback on dispatcher failure would add significant complexity.

Decision

Transactions and rollback mechanisms will not be implemented.

Reason

The worst-case scenario is acceptable for this use case: if the dispatcher succeeds but persistence fails (or vice versa), we would dispatch notifications multiple times once retry logic is implemented. This is a known trade-off and is preferable to the added complexity of transaction management. In a production system, monitoring and alerting can catch and remediate duplicate dispatches.

## DEC-004: No DTO wrapper layer for admin endpoints

Context

The admin dashboard endpoints return domain entities directly from repositories (WorldEvent, AlertRule, NotificationDelivery). In some architectures, a separate DTO layer is used to shield internal domain models from API consumers and provide schema stability.

Decision

No DTO wrapper layer will be created for admin endpoints. Domain entities will be returned directly in API responses.

Reason

Authentication will be implemented to protect the admin endpoints. Since access is restricted to authenticated administrators, schema leaking is not a concern. Creating an additional DTO layer would add unnecessary complexity and maintenance overhead for the MVP. Should the API be opened to external consumers in the future, DTOs can be introduced at that time.

## DEC-005: Simple hardcoded authorization for admin endpoints

Context

The AdminController provides access to sensitive administrative data including all events, alert rules, and delivery statuses. Some level of access control is needed to prevent unauthorized exposure of this information.

Decision

A `SimpleAuthorizationFilterAttribute` will be implemented that checks for a hardcoded admin key in the query string (`?adminKey=admin`). This is the only authorization mechanism for admin endpoints.

Reason

This is a simple MVP-level authorization mechanism intended for demonstration and development purposes. It provides basic protection against casual unauthorized access without the complexity of implementing full authentication/authorization (e.g., JWT, OAuth, role-based access control).
