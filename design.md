## Main Workflow
- A user creates an alert rule.
- A world event is submitted through the API.
- The event is validated and stored.
- Active alert rules are evaluated against the event.
- Matching rules create one notification delivery for each configured channel.
- The notification dispatcher selects the appropriate channel implementation.
- The delivery is marked as sent or failed.
- Administrative endpoints expose events, alert rules, and delivery statuses.
 
## Domain Model
### WorldEvent

WorldEvent represents an event received by the platform, such as breaking news, a market movement, or a natural disaster.

It contains:

- internal identifier;
- external identifier;
- title;
- description;
- category;
- severity;
- occurrence time;
- source;
- optional location.

### AlertRule

AlertRule defines the conditions under which a notification should be created.

It may contain:

- name;
- optional category;
- minimum severity;
- optional keyword;
- optional location;
- active status;
- one or more notification channels.

### AlertChannel

AlertChannel represents one notification destination configured for an alert rule.

It contains:

- channel type;
- destination.

The initially supported channel types are:

- email;
- Slack.

### NotificationDelivery

NotificationDelivery represents one notification attempt created for a matched event and alert rule.

It contains:

- related world event identifier;
- related alert rule identifier;
- notification channel type;
- delivery status;
- creation time;
- optional sent time;
- optional failure reason.