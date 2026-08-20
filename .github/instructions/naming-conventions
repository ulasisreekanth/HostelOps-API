# .NET Naming Conventions

Follow these naming conventions for all C#/.NET code.

## General Rules

- Follow standard Microsoft C# and .NET naming conventions.
- Use clear, meaningful, and descriptive names.
- Prefer clarity over brevity.
- Avoid unnecessary abbreviations.
- Keep terminology consistent throughout the project.
- Do not use names that differ only by casing.
- Avoid unnecessary prefixes and suffixes.
- Avoid generic names such as `Data`, `Info`, `Object`, `Helper`, `Manager`, or `Utility` unless they clearly describe the responsibility.
- Do not rename existing public APIs only for stylistic reasons unless explicitly requested.
- When modifying existing code, follow the established naming convention unless it is clearly incorrect or explicitly requested to change.

## Namespaces

- Use PascalCase.
- Use a consistent namespace hierarchy.

Examples:

`Company.Project`
`Company.Project.Services`
`Company.Project.Infrastructure`

## Classes

- Use PascalCase.
- Use nouns or descriptive noun phrases.
- Avoid unnecessary suffixes such as `Class`, `Object`, or `Implementation`.

Examples:

`UserService`
`OrderProcessor`
`FileRepository`

## Interfaces

- Prefix interfaces with `I`.
- Use PascalCase.

Examples:

`IUserService`
`IRepository`
`IFileProcessor`

## Records

- Use PascalCase.

Examples:

`UserResponse`
`CreateUserRequest`
`Address`

## Structs

- Use PascalCase.

Examples:

`DateRange`
`Money`
`UserIdentifier`

## Enums

- Use PascalCase for enum names.
- Use PascalCase for enum members.

Example:

`ProcessingStatus`

Members:

`Pending`
`Processing`
`Completed`
`Failed`

## Methods

- Use PascalCase.
- Use descriptive action-based names.
- Prefer verbs that clearly describe the operation.
- Avoid vague names such as `DoSomething`, `Process`, or `GetData` when a more specific name is possible.

Examples:

`GetUser()`
`CreateUser()`
`UpdateUser()`
`DeleteUser()`
`ProcessFile()`
`ValidateRequest()`
`CalculateTotal()`

## Async Methods

- Methods returning `Task` or `Task<T>` must use the `Async` suffix.
- Do not use `Async` for synchronous methods.

Examples:

`GetUserAsync()`
`ProcessFileAsync()`
`SaveChangesAsync()`

## Constructors

- Constructor names must exactly match the class name.
- Constructor parameters must use camelCase.

Example:

`UserService(IUserRepository userRepository)`

## Properties

- Use PascalCase.
- Prefer properties over public fields.

Examples:

`UserId`
`CreatedDate`
`Status`
`ItemCount`

## Boolean Properties

- Use names that clearly represent a state, condition, or capability.
- Prefer `Is`, `Has`, `Can`, or `Should` prefixes.

Examples:

`IsActive`
`IsEnabled`
`IsDeleted`
`HasPermission`
`CanProcess`
`ShouldRetry`

Avoid ambiguous names:

`Active`
`Enabled`
`Permission`
`Retry`

## Fields

- Private and protected fields use `_camelCase`.
- Prefer `readonly` where applicable.
- Avoid prefixes such as `m_`.

Examples:

`_userService`
`_logger`
`_retryCount`

## Static Fields

- Follow normal field naming conventions.
- Private static fields use `_camelCase`.

Examples:

`_instanceCount`
`_defaultValue`

## Public Fields

- Avoid public fields unless there is a specific reason.
- Prefer properties.

Prefer:

`public string Name { get; set; }`

Instead of:

`public string Name;`

## Local Variables

- Use camelCase.
- Use meaningful names.
- Avoid meaningless names such as `x`, `obj`, `data`, or `temp` unless their meaning is obvious from the immediate context.

Examples:

`userId`
`customerCode`
`processedItemCount`
`configuration`

## Method Parameters

- Use camelCase.
- Use descriptive names.

Example:

`GetUser(Guid userId, string tenantId)`

## ref, in, and out Parameters

- Follow the same camelCase convention as normal parameters.
- Use descriptive names.

Examples:

`ref int retryCount`
`in RequestOptions options`
`out string result`

## CancellationToken Parameters

- Name cancellation tokens `cancellationToken`.

Example:

`ProcessAsync(Request request, CancellationToken cancellationToken)`

## Constants

- Use PascalCase.
- Do not use uppercase snake case.

Examples:

`MaxRetryCount`
`DefaultTimeout`
`DefaultStatus`

Avoid:

`MAX_RETRY_COUNT`
`DEFAULT_TIMEOUT`

## Generic Type Parameters

- Use `T` for simple generic types.
- Use descriptive PascalCase names when multiple or specialized generic parameters are required.

Examples:

`T`
`TEntity`
`TKey`
`TRequest`
`TResponse`

## Generic Type Constraints

- Use the same generic type parameter naming conventions consistently throughout the declaration.

Example:

`Repository<TEntity, TKey>`

## Delegates

- Use PascalCase.
- Use descriptive names representing the action or purpose.

Examples:

`ProcessingCompletedHandler`
`ValidationHandler`

## Events

- Use PascalCase.
- Name events after the event or state change.
- Do not unnecessarily prefix event names with `On`.

Examples:

`ProcessingCompleted`
`StatusChanged`
`ItemCreated`

## Event Handlers

- Use descriptive names.
- `On` is appropriate for methods that raise or handle events.

Examples:

`OnProcessingCompleted()`
`OnStatusChanged()`

## Extension Methods

- Use PascalCase.
- Name the extension class after the type or functionality being extended.
- Use the `Extensions` suffix.

Examples:

`StringExtensions`
`DateTimeExtensions`
`CollectionExtensions`

Methods should follow normal method naming conventions.

## Static Classes

- Use PascalCase.
- Name the class according to its responsibility.

Examples:

`DateTimeExtensions`
`ValidationExtensions`

Avoid generic names such as:

`CommonHelper`
`Utility`
`GlobalHelper`

## Operators

- Follow standard C# operator syntax.
- Do not create custom operator overloads solely to avoid meaningful method names.
- Operator behavior should be intuitive and consistent with the type.

## Indexers

- Use normal property/indexer conventions.
- Index parameters should use camelCase and meaningful names where appropriate.

Example:

`this[int index]`

## Exceptions

- Use PascalCase.
- Custom exception names must end with `Exception`.
- Name the exception according to the failure condition.

Examples:

`ValidationException`
`ProcessingException`
`ResourceNotFoundException`
`ConfigurationException`

## Attributes

- Use PascalCase.
- Custom attribute classes should normally end with `Attribute`.
- The `Attribute` suffix can be omitted when using the attribute.

Examples:

`AuditAttribute`
`[Audit]`

## Controllers

- Use PascalCase.
- End controller names with `Controller`.
- Name controllers after the resource or responsibility.

Examples:

`UsersController`
`OrdersController`
`ReportsController`

## Services

- Use descriptive names.
- Use the `Service` suffix when appropriate.

Examples:

`UserService`
`EmailService`
`FileProcessingService`

Avoid generic names such as:

`CommonService`
`GeneralService`
`HelperService`

unless the responsibility is genuinely clear.

## Repositories

- Use the entity or resource name followed by `Repository`.

Examples:

`UserRepository`
`OrderRepository`

## Validators

- Use the target type followed by `Validator`.

Examples:

`CreateUserRequestValidator`
`UpdateUserRequestValidator`

## Factories

- Use the object being created followed by `Factory`.

Examples:

`UserFactory`
`ConnectionFactory`

## Options and Configuration

- Use the configuration area followed by `Options`.
- Prefer `Options` for classes bound to configuration sections.

Examples:

`DatabaseOptions`
`StorageOptions`
`AuthenticationOptions`

## Middleware

- Use a descriptive responsibility followed by `Middleware` when appropriate.

Examples:

`ExceptionHandlingMiddleware`
`RequestLoggingMiddleware`
`AuthenticationMiddleware`

## Hosted Services and Background Services

- Use a descriptive responsibility followed by `Service` or `Worker` when appropriate.

Examples:

`CleanupService`
`MessageProcessingService`
`BackgroundWorker`

## DTOs

- Use descriptive names based on their purpose.
- Avoid unnecessary `Dto` suffixes when the purpose is already clear.

Examples:

`UserResponse`
`UserDetailsResponse`
`UserSummary`

If the project consistently uses `Dto`, follow the existing project convention.

## Requests

Use the operation followed by the resource and `Request`.

Examples:

`CreateUserRequest`
`UpdateUserRequest`
`SearchUsersRequest`

## Responses

Use the resource and purpose followed by `Response` where appropriate.

Examples:

`UserResponse`
`UserDetailsResponse`
`UserListResponse`

## Controllers and API Models

Keep naming consistent between controllers, requests, responses, and routes.

Examples:

`UsersController`
`CreateUserRequest`
`UpdateUserRequest`
`UserResponse`

## API Routes

- Use lowercase.
- Prefer resource-oriented routes.
- Use plural resource names where appropriate.
- Use kebab-case for multi-word resources.
- Avoid HTTP verbs in routes when REST conventions apply.

Prefer:

`/api/users`
`/api/users/{id}`
`/api/activity-logs`

Avoid:

`/api/GetUsers`
`/api/create-user`
`/api/getActivityLogs`

## Route Parameters

- Use camelCase.
- Use names consistent with the corresponding model/property where appropriate.

Examples:

`{id}`
`{userId}`
`{customerId}`

## IDs

- Use `Id`, not `ID`.

Prefer:

`userId`
`UserId`
`customerId`
`CustomerId`

Avoid:

`userID`
`UserID`
`customerID`
`CustomerID`

## Acronyms

- Use PascalCase for common acronyms.
- Do not use all-uppercase acronyms in identifiers.

Prefer:

`ApiResponse`
`HttpClient`
`JsonSerializer`
`XmlDocument`

Avoid:

`APIResponse`
`HTTPClient`
`JSONSerializer`
`XMLDocument`

## Abbreviations

- Avoid unnecessary abbreviations.
- Use full words when they improve clarity.

Prefer:

`configuration`
`repository`
`request`
`response`
`customerId`

Avoid:

`config`
`repo`
`req`
`res`
`custId`

Use established .NET terminology and abbreviations when they are standard and unambiguous.

## Collections

- Use plural names for collections when they contain multiple items.
- Use singular names for individual items.

Examples:

`users`
`orders`
`documents`
`user`
`order`
`document`

## Loop Variables

- Use meaningful names when iterating collections.
- Short names such as `i` are acceptable for simple index-based loops.

Examples:

`foreach (var user in users)`
`foreach (var item in items)`

For simple index-based loops:

`for (var i = 0; i < items.Count; i++)`

## Date and Time Variables

Use names that clearly indicate what the value represents.

Examples:

`createdDate`
`updatedDate`
`startDate`
`endDate`
`createdAt`
`updatedAt`
`expirationTime`

Use terminology consistently within the project.

## Local Functions

- Follow the same naming conventions as normal methods.
- Use PascalCase.
- Use descriptive action-based names.

Examples:

`CalculateTotal()`
`ValidateInput()`
`BuildResponse()`

## Test Classes

- Use the class under test followed by `Tests`.

Examples:

`UserServiceTests`
`OrderProcessorTests`

## Test Methods

- Clearly describe the scenario and expected behavior.
- Follow the existing test framework and project convention consistently.

Examples:

`GetUserAsync_ShouldReturnUser_WhenUserExists`
`GetUserAsync_ShouldThrow_WhenUserDoesNotExist`
`CreateUser_ShouldReturnValidationError_WhenRequestIsInvalid`

## File Names

- File names should normally match the primary type.
- Use PascalCase.

Examples:

`UserService.cs`
`IUserService.cs`
`UserController.cs`
`UserResponse.cs`
`ProcessingException.cs`

## Folder Names

- Use PascalCase.
- Keep folder naming consistent with the project structure.

Examples:

`Controllers`
`Services`
`Repositories`
`Models`
`Extensions`
`Middleware`
`Validators`
`Exceptions`
`Configurations`

## Project Names

- Use PascalCase.
- Use descriptive project names.
- Use conventional suffixes where they communicate project responsibility.

Examples:

`Company.Project.Api`
`Company.Project.Application`
`Company.Project.Domain`
`Company.Project.Infrastructure`
`Company.Project.Tests`

## Solution Names

- Use PascalCase.
- Use a meaningful solution name representing the overall application or system.

Example:

`Company.Project.sln`

## Naming Decision Rule

When creating or modifying code:

1. Follow standard .NET/C# naming conventions.
2. Prefer clear and descriptive names.
3. Keep naming consistent throughout the project.
4. Avoid unnecessary abbreviations.
5. Follow existing project terminology when it is already established.
6. Do not rename existing public APIs only for stylistic reasons unless explicitly requested.
7. Prefer consistency with the surrounding code when modifying existing functionality.
8. Apply these conventions primarily to new and modified code.
9. Do not introduce project-specific naming patterns without a clear reason.
10. When multiple valid names are possible, choose the name that best communicates the code's responsibility and intent.
