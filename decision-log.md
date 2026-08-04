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