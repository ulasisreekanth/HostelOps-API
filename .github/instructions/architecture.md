# DDD Architecture & Development Instructions

## 1. Purpose

This document defines generic development standards for building maintainable .NET applications using:

* Domain-Driven Design (DDD)
* Clean Architecture
* Modular Architecture
* SOLID principles
* Separation of Concerns
* Dependency Inversion
* Domain-centric business logic
* Testable and maintainable code

These guidelines are intentionally domain-agnostic and can be reused across different projects and business domains.

---

# 2. Architecture Principles

Follow these core principles:

1. Business logic belongs to the Domain.
2. Application logic belongs to the Application layer.
3. Infrastructure concerns belong to Infrastructure.
4. HTTP/API concerns belong to the Presentation layer.
5. Dependencies should point inward toward the Domain.
6. The Domain must remain independent of technical infrastructure.
7. Business rules must not be implemented only at the API or UI level.
8. Modules should have clear boundaries.
9. Prefer simple solutions over unnecessary abstractions.
10. Design for maintainability before optimizing for theoretical scalability.

---

# 3. Recommended Architecture

Use:

```text
Clean Architecture
+
Domain-Driven Design
+
Modular Architecture
```

A typical application should follow:

```text
Presentation
     |
     v
Application
     |
     v
Domain
     ^
     |
Infrastructure
```

The Domain should be the center of the architecture.

Infrastructure implements interfaces required by the inner layers.

---

# 4. Recommended Solution Structure

A typical .NET solution can use:

```text
Project.sln

Project.API

Project.Domain
Project.Application
Project.Infrastructure

Project.Contracts
Project.SharedKernel
```

For larger applications, organize by business module:

```text
Project.Modules.ModuleA
Project.Modules.ModuleB
Project.Modules.ModuleC
```

Each module may contain:

```text
Domain
Application
Infrastructure
Contracts
```

Do not create projects or folders simply to follow a pattern.

Create them when they provide meaningful separation.

---

# 5. Layer Responsibilities

## 5.1 Presentation Layer

Responsible for:

* HTTP endpoints
* Request/response handling
* Authentication
* Authorization
* Model binding
* API versioning
* HTTP status codes
* API-specific error handling

The Presentation layer should remain thin.

Do not place business rules inside controllers or endpoints.

---

## 5.2 Application Layer

Responsible for application use cases.

Typical responsibilities:

* Commands
* Queries
* Handlers
* DTOs
* Application services
* Input validation
* Authorization orchestration
* Transaction orchestration
* Calling domain behavior
* Coordinating external operations

The Application layer answers:

> What does the application need to do?

It should coordinate the Domain rather than replace it.

---

## 5.3 Domain Layer

The Domain contains the core business model.

Typical components:

```text
Entities
Aggregate Roots
Value Objects
Domain Services
Domain Events
Business Rules
Domain Exceptions
Repository Abstractions
```

The Domain must not depend on:

* ASP.NET Core
* Entity Framework Core
* SQL Server
* HTTP clients
* Azure
* AWS
* External APIs
* UI frameworks
* Infrastructure implementations

The Domain should be testable without a database or external service.

---

## 5.4 Infrastructure Layer

Infrastructure contains technical implementations.

Examples:

```text
Entity Framework Core
SQL Server
Repositories
External APIs
Email
File Storage
Caching
Message Brokers
Payment Providers
Cloud Services
Authentication Providers
```

Infrastructure implements abstractions defined by inner layers.

Example:

```text
IRepository
    ↑
    |
RepositoryImplementation
```

---

# 6. Dependency Rule

Dependencies must point toward the Domain.

Allowed:

```text
API
 ↓
Application
 ↓
Domain
```

Infrastructure can implement abstractions defined by Application or Domain.

Not allowed:

```text
Domain → Infrastructure

Domain → EF Core

Domain → ASP.NET Core

Domain → API
```

The Domain must not know how its data is stored or how external services are implemented.

---

# 7. Domain-Driven Design

DDD should be used to model business behavior, not simply to create additional classes.

The most important DDD concepts are:

```text
Entity
Value Object
Aggregate
Aggregate Root
Domain Service
Domain Event
Repository
Bounded Context
```

Use these concepts only where they provide business value.

---

# 8. Entities

Use an Entity when an object has a distinct identity and lifecycle.

Examples in different systems might include:

```text
Customer
Order
Account
Product
Subscription
Employee
Document
Invoice
```

An Entity should:

* Have a stable identity.
* Protect important state.
* Enforce relevant business rules.
* Control valid state transitions.
* Avoid unnecessary public setters.

Prefer:

```csharp
public class Order
{
    public Guid Id { get; private set; }

    public OrderStatus Status { get; private set; }

    public void Confirm()
    {
        // Validate business rules
        Status = OrderStatus.Confirmed;
    }
}
```

Avoid exposing unrestricted mutation:

```csharp
order.Status = OrderStatus.Confirmed;
```

when the state transition has business rules.

---

# 9. Value Objects

Use Value Objects for concepts defined by their values rather than identity.

Examples:

```text
Money
EmailAddress
PhoneNumber
Address
DateRange
Percentage
Currency
```

Value Objects should generally be:

* Immutable
* Self-validating
* Equality-based on value
* Free from side effects

Example:

```csharp
public sealed record EmailAddress(string Value);
```

Do not create Value Objects for every primitive type without a meaningful reason.

---

# 10. Aggregate Roots

An Aggregate is a consistency boundary.

The Aggregate Root:

* Owns the aggregate boundary.
* Protects invariants.
* Controls modifications to internal entities.
* Defines the transaction boundary.

Example:

```text
Order
 ├── OrderItem
 ├── ShippingAddress
 └── OrderPayment
```

If `Order` is the Aggregate Root, external code should normally interact through `Order`.

Do not expose internal entities for unrestricted modification.

---

# 11. Aggregate Design

Follow these rules:

1. Keep aggregates as small as practical.
2. Protect business invariants inside aggregates.
3. Avoid loading unnecessary object graphs.
4. Use IDs to reference other aggregates.
5. Avoid large aggregates containing unrelated concepts.
6. Prefer one aggregate transaction where possible.
7. Do not make every related entity part of the same aggregate.

Example:

Prefer:

```csharp
public Guid CustomerId { get; private set; }
```

when Customer is another Aggregate Root.

Instead of maintaining a large object graph:

```csharp
public Customer Customer { get; private set; }
```

unless the domain genuinely requires it.

---

# 12. Business Invariants

Business invariants must be protected by the Domain.

Example:

```text
An order cannot be confirmed without at least one item.
```

The rule should be enforced by the domain model.

Do not rely only on:

* Frontend validation
* Controller validation
* Database queries
* UI restrictions

Application validation is useful for input validation, but it must not replace domain invariants.

---

# 13. Domain Services

Use a Domain Service when:

* The logic is genuinely domain-specific.
* The logic does not naturally belong to one Entity or Aggregate.
* Multiple domain concepts are involved.

Examples:

```text
PricingService
EligibilityService
CalculationService
AllocationService
```

Do not create a service for every entity.

Avoid unnecessary classes such as:

```text
CustomerService
OrderService
ProductService
```

when the behavior naturally belongs to the corresponding domain model.

---

# 14. Domain Events

Use Domain Events to represent important business facts.

Examples:

```text
OrderCreated
OrderConfirmed
PaymentReceived
AccountActivated
DocumentApproved
SubscriptionCancelled
```

A Domain Event should represent something that happened.

Example:

```csharp
public sealed record OrderConfirmedDomainEvent(
    Guid OrderId);
```

Do not put infrastructure actions directly into Domain Events.

Avoid:

```text
OrderConfirmed
 ├── SendEmail
 ├── CallExternalAPI
 ├── SaveBlob
 └── PublishMessage
```

Those actions belong to handlers or application/infrastructure components.

---

# 15. Application Events vs Integration Events

Keep these concepts separate.

### Domain Event

Used inside the domain/application boundary to represent a business event.

```text
OrderConfirmed
```

### Integration Event

Used when communicating with another independently deployed system or bounded context.

```text
OrderConfirmedIntegrationEvent
```

Do not automatically publish every Domain Event externally.

Only create integration events when there is a real integration requirement.

---

# 16. Bounded Contexts

Use Bounded Contexts to separate areas of the business that have:

* Different terminology
* Different rules
* Different models
* Different responsibilities

A large system may contain:

```text
Sales
Billing
Identity
Inventory
Reporting
Notifications
```

Each context should own its model.

Do not create a single global domain model for the entire application.

---

# 17. Modular Architecture

For larger applications, organize the system around business capabilities.

Example:

```text
Modules
├── ModuleA
├── ModuleB
├── ModuleC
└── ModuleD
```

Each module should own:

* Business logic
* Application use cases
* Persistence logic
* Contracts

A module should not directly manipulate another module's internal entities or database tables.

---

# 18. Module Communication

Prefer explicit communication between modules.

Possible mechanisms:

```text
Application Contracts
Domain Events
Integration Events
Explicit Interfaces
```

Avoid:

```text
Module A
    ↓
Module B internal database table
```

Prefer:

```text
Module A
    ↓
Contract / Event
    ↓
Module B
```

Avoid circular module dependencies.

---

# 19. Shared Kernel

A Shared Kernel should contain only genuinely shared technical/domain primitives.

Possible examples:

```text
Entity base abstraction
DomainEvent abstraction
Result type
Common value abstractions
```

Do not put business entities from multiple modules into the Shared Kernel.

Do not turn SharedKernel into a generic dumping ground.

---

# 20. Commands

Commands represent operations that change state.

Examples:

```text
CreateOrderCommand
UpdateCustomerCommand
ApproveDocumentCommand
ProcessPaymentCommand
```

Commands should represent business intent.

Prefer:

```text
ApproveDocument
```

over:

```text
UpdateDocumentStatus
```

when approval represents a meaningful business operation.

---

# 21. Queries

Queries retrieve information without changing business state.

Examples:

```text
GetCustomer
GetOrders
GetDashboard
GetReport
GetAvailableItems
```

Queries can use optimized read models or direct projections.

Do not load a complete domain aggregate when only a small read model is required.

---

# 22. CQRS

Use lightweight CQRS where useful.

Separate:

```text
Commands → Change state
Queries  → Read state
```

CQRS does not automatically require:

* Separate databases
* Event sourcing
* Message brokers
* Microservices

Start with simple CQRS within the same application and database.

Introduce advanced CQRS only when justified by requirements.

---

# 23. DTOs

Do not expose Domain Entities directly through APIs.

Use DTOs for boundaries.

Examples:

```text
CreateOrderRequest
UpdateOrderRequest
OrderResponse
OrderSummaryResponse
```

DTOs should contain only the data required by the contract.

Do not reuse one DTO for every operation if the requirements differ.

---

# 24. Repository Pattern

Repositories should provide persistence access for Aggregate Roots.

Examples:

```csharp
IOrderRepository
ICustomerRepository
IPaymentRepository
```

Repositories should not contain business rules.

Avoid unnecessary generic repositories:

```csharp
IGenericRepository<T>
```

Use specific repositories when they provide meaningful domain or persistence behavior.

---

# 25. Persistence

Use EF Core or another ORM in Infrastructure.

Keep persistence concerns outside the Domain.

Prefer separate EF configurations:

```csharp
IEntityTypeConfiguration<Order>
IEntityTypeConfiguration<Customer>
```

Use:

* Primary keys
* Foreign keys
* Unique constraints
* Indexes
* Concurrency control
* Appropriate normalization

Database constraints should provide additional protection for critical invariants.

---

# 26. IDs

Choose identifiers based on the domain and technical requirements.

GUID/UUID is appropriate when:

* IDs may be exposed externally.
* Distributed creation is required.
* Global uniqueness is useful.
* Entities may move between systems.

Numeric IDs may be appropriate when:

* The identifier is purely internal.
* Sequential values provide practical benefits.
* There is no need for distributed generation.

Do not choose GUID simply because DDD requires it.

An identifier should represent identity, not business meaning.

---

# 27. Business Values vs IDs

Do not confuse business values with identifiers.

For example:

```text
Id          → Guid
Code        → string
Sequence    → int
Amount      → decimal
Quantity    → int
```

Choose the type based on what the property represents.

---

# 28. Validation

Use multiple levels of validation.

### Presentation/Application Validation

Validate:

```text
Required fields
Format
Length
Input structure
Basic constraints
```

### Domain Validation

Validate:

```text
Business rules
Business invariants
State transitions
```

### Database Constraints

Protect:

```text
Uniqueness
Relationships
Critical integrity rules
Concurrency
```

Never rely exclusively on client-side validation.

---

# 29. State Transitions

Business state changes should be explicit.

Prefer:

```csharp
order.Confirm();
order.Cancel();
order.Complete();
```

Instead of:

```csharp
order.Status = OrderStatus.Confirmed;
```

This keeps business rules centralized.

---

# 30. Application Services / Handlers

Application services coordinate use cases.

Example:

```text
Request
  ↓
Command Handler
  ↓
Load Aggregate
  ↓
Execute Domain Behavior
  ↓
Persist Aggregate
  ↓
Publish/Process Events
```

Application services should orchestrate.

They should not become large classes containing all business logic.

---

# 31. Controllers / Endpoints

Keep controllers thin.

Example:

```csharp
[HttpPost]
public async Task<IActionResult> Create(
    CreateOrderRequest request,
    CancellationToken cancellationToken)
{
    var result = await _sender.Send(
        new CreateOrderCommand(request),
        cancellationToken);

    return Ok(result);
}
```

Avoid putting:

* Business calculations
* Database queries
* State transitions
* Complex workflows

directly into controllers.

---

# 32. Error Handling

Use consistent error handling.

Distinguish between:

```text
Validation Error
Business Rule Error
Not Found
Conflict
Unauthorized
Forbidden
Infrastructure Failure
Unexpected Failure
```

Do not expose internal stack traces or infrastructure details to clients.

Use a consistent error contract.

Example:

```json
{
  "code": "ORDER_NOT_FOUND",
  "message": "The requested order was not found."
}
```

---

# 33. Exceptions

Do not use exceptions for normal business control flow when a Result pattern provides clearer behavior.

Use exceptions for:

* Unexpected failures
* Infrastructure failures
* Exceptional conditions

Expected business outcomes may use:

```text
Result
Result<T>
Error
```

where appropriate.

---

# 34. Transactions

Transactions should align with business consistency boundaries.

Prefer:

```text
One command
    ↓
One business transaction
```

Avoid unnecessarily large transactions spanning unrelated operations.

Do not introduce distributed transactions unless there is a strong requirement.

---

# 35. Concurrency

Design critical operations for concurrent access.

Potential scenarios:

```text
Two users updating the same record
Two users attempting the same operation
Duplicate requests
Concurrent background processing
```

Use appropriate mechanisms:

```text
Optimistic concurrency
Concurrency tokens
Database constraints
Transactions
Atomic operations
```

Application-level checks alone are not always sufficient.

---

# 36. Idempotency

Use idempotency for operations that can be retried.

Especially important for:

```text
Payments
Webhooks
Message processing
External API calls
Background jobs
```

Repeated requests should not accidentally create duplicate business operations.

Use an idempotency key or equivalent mechanism where appropriate.

---

# 37. External Services

External services should be abstracted.

Examples:

```csharp
IEmailSender
IFileStorage
IPaymentProvider
IExternalCustomerService
```

Infrastructure provides the implementation.

The Domain should never directly call an external provider.

---

# 38. Async Programming

Use asynchronous programming for I/O operations.

Prefer:

```csharp
Task<T>
Task
CancellationToken
```

Avoid:

```csharp
.Result
.Wait()
```

Avoid unnecessary:

```csharp
Task.Run()
```

for I/O-bound operations.

Pass `CancellationToken` through application and infrastructure operations where appropriate.

---

# 39. Dependency Injection

Use constructor injection.

Prefer:

```csharp
public class OrderHandler
{
    private readonly IOrderRepository _repository;

    public OrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
}
```

Avoid service locator patterns.

If a class requires a very large number of dependencies, reconsider its responsibilities.

---

# 40. Security

Follow secure-by-default principles.

Always consider:

* Authentication
* Authorization
* Input validation
* Secure secret storage
* HTTPS
* Rate limiting
* CORS
* Secure headers
* Audit logging
* Data protection

Never trust client-provided authorization information.

Do not accept identity or permission information from the client as authoritative.

---

# 41. Authorization

Authentication determines:

> Who is the user?

Authorization determines:

> What is the user allowed to do?

Authorization should be enforced server-side.

Use appropriate mechanisms such as:

```text
Roles
Permissions
Policies
Resource-based authorization
```

Do not rely on frontend restrictions for security.

---

# 42. Logging

Use structured logging.

Log:

* Important business operations
* Errors
* Warnings
* Integration failures
* Background job failures
* Security-relevant events

Do not log:

```text
Passwords
Access tokens
Secrets
Connection strings
Payment credentials
Sensitive personal data
```

Use correlation IDs where appropriate.

---

# 43. Metrics

Collect useful operational metrics.

Examples:

```text
Request count
Request duration
Error rate
Database latency
External API latency
Queue length
Background job failures
```

Business metrics can be collected separately.

Do not put monitoring concerns directly into domain entities.

---

# 44. Distributed Tracing

Use distributed tracing for important request flows.

Example:

```text
API
 ↓
Application
 ↓
Database
 ↓
External Service
```

Use OpenTelemetry or the organization's standard observability solution.

---

# 45. Auditing

Audit important business and security operations.

Typical information:

```text
Who
What
When
Entity
Action
Correlation ID
```

Audit records should be treated as historical records.

Do not modify historical audit data without a defined compliance requirement.

---

# 46. Time Handling

Use UTC for internal timestamps unless the business explicitly requires another representation.

Prefer:

```csharp
DateTimeOffset
```

for timestamps where timezone/offset information matters.

Convert to user-specific timezone at the presentation boundary.

Do not rely on the server's local timezone.

---

# 47. Financial Values

Never use floating-point types for monetary calculations.

Use:

```csharp
decimal
```

Define explicit:

* Currency
* Precision
* Rounding rules

Centralize important financial calculations.

Do not duplicate financial logic across controllers, services, and repositories.

---

# 48. Reporting and Read Models

Reporting and dashboards are generally read-heavy operations.

Do not load large domain aggregates simply to produce reports.

Prefer:

```text
Query
 ↓
Projection
 ↓
DTO
```

Use optimized queries where required.

Reporting requirements should not unnecessarily influence aggregate design.

---

# 49. Pagination

Use pagination for potentially large collections.

Support where appropriate:

```text
Page
PageSize
Sort
Filters
```

Avoid returning unbounded collections.

---

# 50. Caching

Use caching only when there is a measurable benefit.

Good candidates may include:

```text
Reference data
Configuration
Permissions
Frequently accessed read models
```

Always define an invalidation strategy.

Consider authorization and tenant/context boundaries when generating cache keys.

---

# 51. Configuration and Secrets

Never hardcode:

```text
Passwords
API keys
Connection strings
Tokens
Secrets
Environment-specific credentials
```

Use appropriate configuration and secret-management mechanisms.

Configuration should be environment-specific and externally managed.

---

# 52. Feature Flags

Feature flags may be used for controlled rollout.

Examples:

```text
EnableNewFlow
EnableNewFeature
EnableExperimentalCapability
```

Avoid spreading feature-flag checks throughout Domain entities.

Keep feature rollout concerns primarily in Application/Presentation orchestration.

---

# 53. Testing Strategy

Use a testing pyramid:

```text
           E2E
          /   \
   Integration
       /       \
   Unit Tests
```

## Unit Tests

Focus heavily on:

* Domain behavior
* Business rules
* Aggregates
* Value Objects
* Calculations
* State transitions

## Integration Tests

Test:

* Database behavior
* EF Core mappings
* Repositories
* External integrations
* Module boundaries

## API Tests

Test:

* Authentication
* Authorization
* Validation
* HTTP behavior
* Response contracts

## End-to-End Tests

Use for critical business workflows.

---

# 54. Test Naming

Tests should describe behavior.

Prefer:

```text
ConfirmOrder_WhenOrderIsValid_ShouldConfirmOrder
```

```text
ConfirmOrder_WhenOrderHasNoItems_ShouldFail
```

```text
ProcessPayment_WhenRequestIsRetried_ShouldNotCreateDuplicatePayment
```

Avoid test names that describe implementation details.

---

# 55. Domain Test Independence

Domain tests should not require:

```text
Database
HTTP
Cloud services
External APIs
File storage
Message brokers
```

Domain tests should be fast and deterministic.

---

# 56. Performance

Do not optimize prematurely.

Follow:

```text
Correctness
    ↓
Maintainability
    ↓
Measure
    ↓
Optimize
```

For performance-sensitive queries consider:

* Projection
* Pagination
* Proper indexes
* AsNoTracking
* Avoiding N+1 queries
* Efficient SQL
* Appropriate caching

Optimize based on measurements rather than assumptions.

---

# 57. Database Access

Do not access the database directly from:

```text
Controllers
Domain Entities
Value Objects
```

Database access should be handled through appropriate Application/Infrastructure boundaries.

Avoid excessive database calls.

Prefer efficient projections for read-only operations.

---

# 58. Code Quality

Follow:

* SOLID
* DRY
* KISS
* YAGNI
* Separation of Concerns
* Dependency Inversion

However:

> Do not apply a principle mechanically when it makes the code harder to understand.

Readable and maintainable code is more important than maximizing abstraction.

---

# 59. Naming

Use names that represent business meaning.

Prefer:

```text
CreateOrderCommand
ApproveDocumentCommand
ProcessPaymentCommand
CalculatePrice
ConfirmOrder
```

Avoid vague names:

```text
Helper
Utility
Manager
Processor
CommonService
DataService
Handler
```

unless the name accurately describes the responsibility.

---

# 60. Code Smells to Avoid

Avoid:

```text
Fat Controllers
Fat Services
Anemic Domain Models
Generic Repositories Everywhere
God Classes
God Services
Static Global State
Service Locator
Direct Database Access from Controllers
Cross-Module Database Access
Circular Dependencies
Duplicated Business Rules
Hardcoded Secrets
Unbounded Queries
Large Aggregates
Unnecessary Abstractions
Premature Microservices
```

---

# 61. Avoid Anemic Domain Models

Do not create entities that only contain properties when those entities have meaningful business behavior.

Prefer:

```csharp
order.Confirm();
order.Cancel();
order.AddItem(item);
```

instead of forcing all behavior into:

```text
OrderService
```

However, do not force unrelated behavior into entities simply to avoid an anemic model.

DDD is about meaningful domain modeling, not maximizing code inside entities.

---

# 62. Avoid Overengineering

Do not introduce a pattern simply because it exists.

Do not automatically add:

```text
Factory
Builder
Specification
Mediator
Repository
CQRS
Event Sourcing
Message Broker
Microservice
```

Use a pattern when it solves an actual problem.

Prefer the simplest architecture that satisfies current requirements while keeping reasonable future flexibility.

---

# 63. Microservice Readiness

If the application is initially a Modular Monolith, maintain clear module boundaries.

A module should have:

* Clear responsibility
* Clear domain model
* Clear contracts
* Controlled dependencies
* Owned persistence
* Independent business rules

This makes future extraction into a microservice possible.

Do not introduce microservices only for the sake of scalability.

Extract services when justified by:

* Independent scaling
* Independent deployment
* Team ownership
* Fault isolation
* Technology requirements
* Clear bounded context boundaries

---

# 64. Pull Request Review Checklist

Review every PR for:

## Architecture

* [ ] Dependencies point in the correct direction.
* [ ] Domain is independent from infrastructure.
* [ ] Business logic is in the correct layer.
* [ ] Module boundaries are respected.

## Domain

* [ ] Business invariants are protected.
* [ ] Aggregate boundaries are appropriate.
* [ ] State transitions are explicit.
* [ ] Value Objects are used where they provide real value.

## Application

* [ ] Use cases are clear.
* [ ] Commands and Queries are separated where appropriate.
* [ ] DTOs are used at boundaries.

## Infrastructure

* [ ] Persistence concerns are isolated.
* [ ] External services are abstracted.
* [ ] Database operations are efficient.

## Security

* [ ] Authorization is enforced server-side.
* [ ] Sensitive information is protected.
* [ ] Secrets are not committed.
* [ ] Resource access is properly authorized.

## Testing

* [ ] Domain rules are tested.
* [ ] Important integration behavior is tested.
* [ ] Critical API workflows are covered.

## Observability

* [ ] Important operations are observable.
* [ ] Errors are logged appropriately.
* [ ] Sensitive information is not logged.

---

# 65. Final Architecture Rule

The most important rule is:

> Model the business domain first, then organize the technical implementation around it.

Use DDD to make business rules explicit.

Use Clean Architecture to control dependencies.

Use Modular Architecture to control boundaries.

Use CQRS where it improves clarity.

Use events where they provide meaningful decoupling.

Use abstractions where they solve real problems.

Do not introduce complexity simply because a pattern is considered an industry standard.

The architecture should remain:

```text
Business-focused
    +
Maintainable
    +
Testable
    +
Secure
    +
Observable
    +
Modular
    +
Practical
```

The goal is not to implement every DDD pattern.

The goal is to build a system where the code clearly represents the business and can evolve safely over time.
