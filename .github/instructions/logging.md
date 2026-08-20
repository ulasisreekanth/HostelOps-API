# Application Logging & Observability Instructions

## 1. Purpose

Follow these standards for logging and observability across all applications, APIs, services, workers, background jobs, and serverless applications.

The implementation may use Azure Application Insights or another configured observability platform.

The standards must remain **domain-independent** and should not contain project-specific business terminology.

---

## 2. Log Levels

Use log levels consistently.

### Information

Use for important and successful application or business events.

Examples:

* Operation started
* Operation completed
* Important configuration loaded
* Background process completed

### Warning

Use when something unexpected occurs but the application can continue.

Examples:

* Resource not found when expected
* Validation failed
* Retry initiated
* External dependency is slow
* Fallback behavior was used

### Error

Use when an operation fails and requires investigation.

Examples:

* Database operation failed
* External API failed
* File operation failed
* Background operation failed

### Critical

Use only when a serious failure affects the application or a critical service.

Examples:

* Application cannot start
* Critical dependency unavailable
* Service cannot initialize
* Required infrastructure unavailable

### Debug

Use for detailed troubleshooting information.

Avoid excessive Debug logging in production.

### Trace

Use only when extremely detailed execution information is required for troubleshooting.

---

## 3. What Should Be Logged

Log **meaningful events**, not every line of code.

Good logging should help answer:

* What happened?
* Why did it happen?
* Which operation was affected?
* Which resource was involved?
* Was the operation successful?
* How long did it take?
* What caused the failure?

Do not add logs simply because a method was entered or exited.

---

## 4. Structured Logging

Always prefer structured logging.

Use searchable properties such as:

* `OperationName`
* `CorrelationId`
* `RequestId`
* `UserId`
* `TenantId`
* `ResourceId`
* `Status`
* `Duration`
* `Environment`
* `ServiceName`

Only include properties relevant to the operation.

### Preferred

```csharp
_logger.LogInformation(
    "Operation completed successfully. ResourceId: {ResourceId}",
    resourceId);
```

### Avoid

```csharp
_logger.LogInformation(
    $"Operation completed successfully for resource {resourceId}");
```

Structured properties allow Application Insights to filter and query telemetry more effectively.

---

## 5. Correlation ID

Maintain correlation context for every request or operation that can cross service boundaries.

Example:

```text
Client
   ↓
API
   ↓
Message Queue
   ↓
Worker
   ↓
External Service
   ↓
Database / Storage
```

The complete operation should be traceable using the same correlation context.

Use the observability platform's built-in distributed tracing capabilities whenever possible.

---

## 6. Distributed Tracing

Use distributed tracing for operations involving multiple services or components.

Trace:

* HTTP requests
* Service-to-service calls
* Message processing
* Background operations
* Database operations
* Storage operations
* External API calls

The objective is to identify:

* Where the operation started
* Where it spent time
* Where it failed
* Which dependency caused the problem

---

## 7. Automatic Telemetry

Do not manually log telemetry that the observability platform already captures.

Where supported, rely on automatic tracking for:

* HTTP requests
* Response status
* Request duration
* Exceptions
* Database dependencies
* HTTP dependencies
* Storage dependencies
* Other supported dependencies

Add custom logs only when additional application or business context is required.

---

## 8. Exception Logging

Log unexpected exceptions with useful context.

Include, where applicable:

* Exception
* Operation name
* Correlation ID
* Resource ID
* User/Tenant context
* Relevant business context

Example:

```csharp
try
{
    // operation
}
catch (Exception ex)
{
    _logger.LogError(
        ex,
        "Unexpected error while executing operation. ResourceId: {ResourceId}",
        resourceId);

    throw;
}
```

Do not log the same exception repeatedly at every application layer.

---

## 9. Warning vs Error

Use **Warning** when the situation is handled and the application can continue.

```text
Validation failed
Resource not found
Retry initiated
Fallback used
```

Use **Error** when an operation has actually failed.

```text
Database operation failed
External service call failed
File processing failed
Background operation failed
```

Use **Critical** only for severe application/service failures.

---

## 10. Metrics

Use metrics for values that need aggregation and trend analysis.

Examples:

* Request count
* Error count
* Error rate
* Response time
* Processing duration
* Queue processing time
* Success/failure count
* Resource utilization
* Business KPIs

Do not generate thousands of logs when a metric is more appropriate.

---

## 11. Custom Events

Use custom events for important business or application events that need independent tracking.

Examples:

```text
OperationStarted
OperationCompleted
OperationFailed
EntityCreated
EntityUpdated
EntityDeleted
ProcessCompleted
ExportCompleted
```

Use generic event names where possible and add project-specific events only when required.

---

## 12. Dependency Monitoring

Monitor important dependencies such as:

* Databases
* Cache systems
* Object/blob storage
* Message queues
* External APIs
* AI/ML services
* Email services
* Third-party services

Use automatic dependency tracking whenever available.

Monitor:

* Duration
* Success/failure
* Dependency type
* Operation
* Exceptions

---

## 13. API Monitoring

For APIs, monitor:

* Request count
* HTTP status codes
* Response time
* Failure rate
* Slow requests
* Availability

Do not manually duplicate request telemetry that is already captured automatically.

---

## 14. Background Jobs and Workers

For background processes, log meaningful lifecycle events.

Examples:

```text
JobStarted
JobCompleted
JobFailed
JobRetried
JobCancelled
```

Where applicable, capture:

* Job/operation ID
* Duration
* Items processed
* Items failed
* Retry count
* Status

---

## 15. Availability Monitoring

Monitor critical services and endpoints using health checks or availability tests.

Availability monitoring should answer:

> Is the application reachable and functioning?

Availability monitoring should be treated separately from normal application logs.

---

## 16. Alerts

Create alerts only for actionable conditions.

Examples:

* High error rate
* Critical exceptions
* Service unavailable
* Dependency failures
* Significant response-time degradation
* Background job failures
* Queue processing failures

Do not create alerts for every Warning or Error.

---

## 17. Dashboards

Important applications should have dashboards showing overall health.

Recommended information:

* Request volume
* Error rate
* Response time
* Exceptions
* Dependency health
* Availability
* Background process health
* Important metrics

---

## 18. Audit Logging

Use audit logs for actions that require traceability.

Record, where applicable:

* Who performed the action
* What action was performed
* When it occurred
* Which resource was affected
* Relevant context

Audit logging is separate from normal diagnostic logging.

---

## 19. Sensitive Data

Never log:

* Passwords
* Access tokens
* Refresh tokens
* API keys
* Secrets
* Connection strings
* Encryption keys
* Sensitive personal information
* Confidential data that is not required for troubleshooting

Always follow data-minimization principles.

---

## 20. Environment and Service Identification

Telemetry should identify the application/service and environment.

Use consistent values such as:

```text
ServiceName: MyApplication.API
Environment: Development
Environment: QA
Environment: Staging
Environment: Production
```

This prevents telemetry from different applications or environments from becoming difficult to distinguish.

---

## 21. Sampling and Retention

Use appropriate telemetry sampling and retention.

The objective is to:

* Preserve important telemetry
* Reduce unnecessary ingestion
* Avoid excessive storage
* Control observability costs

Important errors, critical events, and required audit information must not be unnecessarily discarded.

---

## 22. Cost Monitoring

Regularly monitor observability costs.

Review:

* Telemetry ingestion
* High-volume services
* High-volume log categories
* Debug/Trace usage
* Sampling
* Retention

If telemetry volume increases unexpectedly, identify the source before increasing the budget.

---

## 23. General Rules

### DO

* Use meaningful logs.
* Use appropriate log levels.
* Use structured properties.
* Maintain correlation context.
* Use distributed tracing.
* Monitor dependencies.
* Use metrics for aggregated values.
* Capture important events.
* Create actionable alerts.
* Protect sensitive data.
* Monitor telemetry volume and cost.

### DON'T

* Log every method entry/exit.
* Log everything as Information.
* Use Error for normal handled conditions.
* Use Critical for ordinary failures.
* Duplicate automatic telemetry.
* Log secrets or credentials.
* Log the same exception at every layer.
* Enable excessive Debug/Trace logging in production.
* Create alerts for every log.
* Use logs as a replacement for metrics or tracing.

---

## 24. Standard Observability Model

All projects should follow this general model:

```text
                         APPLICATION
                              │
          ┌───────────────────┼───────────────────┐
          ↓                   ↓                   ↓
        LOGS               METRICS              TRACES
          │                   │                   │
          ↓                   ↓                   ↓
     Exceptions          Performance        Dependencies
     Warnings             KPIs              Service Calls
     Events              Counts             Request Flow
          │                   │                   │
          └───────────────────┼───────────────────┘
                              ↓
                     AVAILABILITY
                              ↓
                          ALERTS
                              ↓
                        DASHBOARDS
                              ↓
                   RETENTION / SAMPLING
                              ↓
                     COST MONITORING
```

## 25. Core Principle

Use the right observability mechanism for the right question:

| Question                                  | Use                  |
| ----------------------------------------- | -------------------- |
| What happened?                            | Logs                 |
| How often?                                | Metrics              |
| How long?                                 | Metrics / Traces     |
| Where did it happen?                      | Distributed Tracing  |
| Which dependency caused it?               | Dependency Telemetry |
| What business/application event occurred? | Custom Events        |
| What failed?                              | Exceptions           |
| Is the service available?                 | Availability         |
| Does someone need to act?                 | Alerts               |
| What is the current health?               | Dashboards           |
| How much telemetry should be stored?      | Sampling / Retention |
| How much does observability cost?         | Cost Monitoring      |

**Keep this document generic. Project-specific logging requirements should be added separately without changing these core standards.**
