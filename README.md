# SimpleBanking

A simple banking system with simple deposit and withdraw mechanism.

## Requirements

- A user can have as many accounts as they want.
 
- A user can create and delete accounts.
 
- A user can deposit and withdraw from accounts.
 
- An account cannot have less than $100 at any time in an account.
 
- A user cannot withdraw more than 90% of their total balance from an account in a single transaction.
 
- A user cannot deposit more than $10,000 in a single transaction.

## Discussion
- Onion architecture with CQRS is applied. Mediatr is used.
- EFCore EntityFramework is used with InMemory option.
- Unit of work pattern is applied.
- Authentication and authorization are not considered. It is assumed that the Gateway injects user IDs to either route or body properly.
- REST style is not followed. RPC like naming is used for endpoints. Simply Get and Post calls are used.

- Implementing a user aggregate having a list of accounts is not considered logical with the given requirements. This service is considered as a microservice, and creating a user notion with a possible profile data would extend the service to be an Identity Server. Only user ids are stored within Account entities. 

- Command and query layer seperated. For commands, Entity Framework tracked entities are used(room to improve, disconnected graph can be used as well). Queries use AsNoTracking to increase performance.
- A seperate storage class is not created(room to improve). Instead directly domain objects are used when configuring the storage entities.

- Positive and negative testing are applied.
- Domain in isolation is not tested(room to improve). Rather dotnet integration testing framework used to test all layers in a single test project.

- Exception middleware is used.

## Usage
- Simply run `docker compose up` from the main directory of the project. After successfully run, check the ports via `docker container ps`. Use the one of the ports that are redirected to either 80 or 443(Sometimes port 80 might cause a problem due to browser settings, the port that is redirecting to 443, is recommended)

- Running localhost:{portnumber}/swagger on a browser will redirect to the swagger page. You can test the endpoints. Enjoy simple banking!







