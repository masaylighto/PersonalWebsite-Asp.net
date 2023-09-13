### Status
[![Deploy to Amazon ECS](https://github.com/masaylighto/TheWayToGerman-Asp.net/actions/workflows/AwsProduction.yml/badge.svg)](https://github.com/masaylighto/TheWayToGerman-Asp.net/actions/workflows/AwsProduction.yml)
### Production Service
[TheWayToGerman](https://www.derwegzumdeutsch.land/)
### Related Projects
[FrontEnd](https://github.com/masaylighto/TheWayToGerman-SolidJS) Written in SolidJs.
### Description 
This Website made to provide information about legal immigration to germany 
### Before you start
If you have a question on how specific parts work, please read its corresponding file in Diagram folder.
Lib contain there own readme file in there folders
### Technology 
1. Asp.net in dotnet 7
2. Docker
3. Postgresql
### Design Patterns And Architectures
1. Mediator
2. Cqrs
3. Repository
4. UnitOfWOrk
5. Exception As Value
6. N-Layer Architecture
### Project Parts
1. Lib: Contain Helper libraries
   - 1.1. Custom Extensions methods.
      - 1.1.1. Expressions Method :: Contain method that modify Expressions
   - 1.2. Cqrs: Contain Class that use Mediator and offer a cqrs pattern
   - 1.3. DataKit: offer class that handle data like data wrapper (Result Struct)
2. Diagram: Contain Diagram for website features 
1. 3. Test: Contain Unit-test and Integration-test 
1. 4. TheWayToGerman: Contain the main Code and seperated to the following
   - 4.1 Core: Contain Core Stuff like Entities,DBContext etc..
   - 4.2 DataAccess: Represent the data access layer and contains repostiores
   - 4.3 Logic: Command And Queries Handlers, Services and anything related to Business logic
   - 4.4 Api: Controllers, DTO, Response Object and middleware
### Design Decision Changes (that happen durring the works)
1.Remove detailed diagram for every function  as the are the same almost just a crud, i create general diagram for the dataflow instead to shorten the time


2.Unit-test deleted. because i already test the entire logic with integration test and i develop this site alone and I don't have the time to add more useless test( as this app is small and its just crud application nothing much to be tested)


3.DTO remove and use mediator Request instead of it. as it make more indirection and the app is simple all dto where just mapped into request so dto where unnecessary
### Build The Project Requests
1. dotnet 7
2. Docker (preferable) and if Docker does not exist you will need to install all dependencies specified in docker-compose 
### How to build
1. If docker exist you can execute ```docker-compose up``` on the docer compose file
2. if docker is not install all dependencies specified in docker-compose configure their ports in app appsettings then run the Api Project With ```dotnet run ``` or publish it witt ```dotnet publish```
