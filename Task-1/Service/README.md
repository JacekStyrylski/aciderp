# Introduction

This part is about creating Simple web application

## Create a simple API of a web application for trip management that allows to

- Create / edit / cancel a trip
- Create / edit a customer
- Assign a customer to a trip

## Assumptions

- Application should work in Microsoft Azure environment
    - It doesn’t have to be deployed, i.e. it may work on local .NET SDK, CosmosDB emulator or
Azure Function emulator etc.

- Application should have 2 simple business domain rules:
   - A customer cannot cancel a trip within 7 business days before its start
   - A customer cannot have two overlapping trips
- Data can be stored in any storage type of your choosing (SQL / CosmosDB / Azure Storage)
- You are free to choose frameworks, libraries and architecture
    - You may use any building blocks (App Service, Azure Functions, Docker containers, etc.)
- The API should be implemented using best practices
- (optional) Add UI to your API
- (optional) Authentication
    - Authentication and authorization should be based on Azure Active Directory
    - Roles: guest (anonymous) user / admin
        - Guest can only view
        - User can edit and delete only own entries
        - Administrator can change everything
- (optional) Use any external REST API (e.g. https://restcountries.eu/) as a source for countries in your
application

## Assessment

The focus of the review is your architecture decisions and implementation. Keep that in mind so you do not
overcomplicate your solution and spend too much time on it – it does not have to be your perfect app.
Ideally, don’t spend more than 8 hours to complete this task. We are happy to see your best practices,
letting us evaluate your experience. If you want to, feel free to show us your creativity.
