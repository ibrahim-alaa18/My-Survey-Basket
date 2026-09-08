# MySurveyBasket Revision Roadmap

This roadmap is meant to help you revise the project in the most useful order, moving from the big picture to the implementation details.

## Recommended Order

### 1. Start With App Startup

Read these files first:

- `Program.cs`
- `DependencyInjection.cs`

Why start here:

- You get the full application skeleton first.
- You see how services are registered.
- You understand middleware order, authentication setup, Swagger, Hangfire, logging, validation, caching, and database wiring.

Questions to answer while reading:

- How does the app boot?
- Which middleware runs for each request?
- How are authentication and authorization configured?
- Which services are injected into the app?

### 2. Understand the Domain and Database

Read these next:

- `Entities/`
- `Persistence/ApplicationDbContext.cs`
- `Persistence/EntitiesConfigurations/`
- `Persistence/migrations/` if you want schema history

Why this comes second:

- It tells you what the system actually stores.
- You understand the relationships between polls, questions, answers, votes, and users.
- You see uniqueness rules and database constraints.

Main things to understand:

- `Poll` contains survey metadata.
- `Question` belongs to a poll.
- `Answer` belongs to a question.
- `Vote` represents one user's submission for one poll.
- `VoteAnswer` links a vote to selected answers per question.
- `RefreshToken` is owned by `ApplicationUser`.

### 3. Revise Authentication and Authorization

Read these after the data model:

- `Authentication/IJwtProvider.cs`
- `Authentication/JwtProvider.cs`
- `Authentication/JwtOptions.cs`
- `Authentication/Filters/`
- `Abstractions/Consts/Permissions.cs`
- `Abstractions/Consts/DefaultRoles.cs`
- `Abstractions/Consts/DefaultUsers.cs`

Why this area matters:

- Authentication is central to almost every feature.
- This project uses both role-based and permission-based authorization.
- It is one of the most important areas to re-understand before changing behavior.

Focus on:

- How JWT tokens are generated
- What claims are stored in the token
- How refresh tokens work
- How custom permissions are enforced
- How roles and permissions are seeded into the database

### 4. Read Features as Controller + Service Pairs

Study each feature vertically:

#### Auth

- `Controllers/AuthController.cs`
- `Services/AuthService.cs`

Learn:

- login
- register
- email confirmation
- refresh token flow
- password reset flow

#### Account

- `Controllers/AccountController.cs`
- `Services/UserService.cs`

Learn:

- profile retrieval
- profile update
- password change

#### Polls

- `Controllers/PollController.cs`
- `Services/PollService.cs`

Learn:

- poll CRUD
- publish/unpublish behavior
- current poll filtering

#### Questions

- `Controllers/QuestionController.cs`
- `Services/QuestionService.cs`

Learn:

- adding questions
- updating answers
- toggling question status
- caching available questions

#### Voting

- `Controllers/VotesController.cs`
- `Services/VoteService.cs`

Learn:

- how a member gets available questions
- how a vote is validated
- how duplicate voting is prevented

#### Results

- `Controllers/ResultController.cs`
- `Services/ResultService.cs`

Learn:

- raw vote reporting
- votes per day
- votes per question and answer

### 5. Review Contracts and Validators

Read these after you understand the feature flows:

- `Contracts/Authentication/`
- `Contracts/Poll/`
- `Contracts/Question/`
- `Contracts/Vote/`
- `Contracts/Results/`
- `Contracts/Users/`

Why this step matters:

- These files define the request and response shapes.
- Validators show the intended business rules for incoming data.
- They help you understand what the API expects from clients.

### 6. Finish With Supporting Infrastructure

Read these last:

- `Mapping/MappingConfigrations.cs`
- `Errors/`
- `Helpers/EmailBodyBuilder.cs`
- `Templates/`
- `Settings/MailSettings.cs`
- `appsettings.json`
- `appsettings.Development.json`

Why this is last:

- These files support the main flow but are easier to understand once the full system makes sense.

## Best Practical Study Strategy

For each feature, use this mini-process:

1. Read the controller to see the endpoint surface.
2. Read the service to understand the business logic.
3. Read the related contract and validator files.
4. Check the related entity or EF configuration if needed.

That way, you study the project by behavior instead of by folder only.

## Suggested First Session

If you want the most effective first revision session, use this order:

1. `Program.cs`
2. `DependencyInjection.cs`
3. `Persistence/ApplicationDbContext.cs`
4. `Entities/`
5. `Authentication/`
6. `Controllers/AuthController.cs`
7. `Services/AuthService.cs`

This gives you the app lifecycle, the data model, and the full auth flow early.

## My Recommendation

If you want a strong starting point, begin with startup and then move directly into authentication.

Best first path:

1. `Program.cs`
2. `DependencyInjection.cs`
3. `Authentication/`
4. `ApplicationDbContext.cs`
5. `Entities/`

That path will rebuild your high-level understanding quickly.
