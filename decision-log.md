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