# Tycoon Factory Application
This is a scheduling application for Tycoon Co. factory's android workers.

## Scenario Description
The application allows users to create, modify, delete, and get activities for the factory's android workers. Activities are categorized as "Build component," which can only be performed by one worker, and "Build machine," which can be performed by one or more workers.
Each android worker is identified by a letter of the alphabet, and they need to rest for 2 hours after building a component and 4 hours after building a machine.

## Solution Scope
The application allows users to schedule activities in the past or future, and conflicts between workers' activities are flagged to help users adjust their schedules.
The top 10 androids working the most hours in the next 7 days are displayed.

## Initial Values and Seed Data
The application comes with initial data about the android workers and some pre-assigned activities.

## Implementation Details
The application consists of three projects:

* Core project: Implements the domain models, interfaces, and business logic using TDD.
* Persistence project: Implements the data access layer using TDD.
* API project: Implements the external interface of the application as a Web API using TDD.
The application follows the SOLID principles and the separation of concerns.

The application has a high-test coverage, and the tests are written using TDD.

The application uses Entity Framework as the ORM for persistence.

## Running the Application (**IMPORTANT**)
* The application can be run on any machine by cloning the repository and restoring the NuGet packages.
* In the Package Manager Console we need to execute the command update-database.
* This endpoint: POST /api/login allows you to obtain a JWT (JSON Web Token) for authenticating your requests to our API.
 - Request :
   To obtain a JWT, you should send a POST request to the following endpoint:
   POST /api/login
   The request body should contain the following JSON payload (These values are hardcoded because this was not implemented, so do not change it):
   {
  "username": "user1",
  "password": "user1",
  "role": "Admin"
   }
  - Response
   The server will respond with a valid JWT.

   You need to copy the JWT and use it for authorize the Factory endpoints. Following these instructions:
   Copy the JWT
   Click on Authorize
   Write Bearer + JWT.

 * After that, the endpoints are ready to be executed.

## Future Improvements
Implement a better user interface for the application.
Implement a full persistence layer to make the application more robust.

## Acknowledgments
Thanks for the opportunity to work on this project.
