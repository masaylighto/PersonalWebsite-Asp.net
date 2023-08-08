# Core.Cqrs
This library use MediatR and offer a class to implement Cqrs Design Pattern
## Classes 
1. BaseHandler: is a abstract class that used as a base for cqrs class
    - To implement it you inhert it from class and overide the Execute method
    - The class return values as result struct which contain either the value or an exception (more about result in Core.DataKit)
    - It use fluentvalidation if you want to use a fluentvalidation class you specific instance as a value to Validator property in the class that inhert from BaseHandler
    example 
```csharp
        public class CustomHandler<Request, Response> : BaseHandler<Request, Response> where Request: IRequest<Result<Response>>
        {
            class CustomValidator : AbstractValidator<Request>
            {
                public CustomValidator()
                {
                   // your rules
                }
            }
            public CustomHandler()
            {
                Validator = new CustomValidator();
            }
            protected override async Task<Result<Response>> Execute(Request request, CancellationToken cancellationToken)
            {
             /// your logic
            }
        }
```
2. CommandHandler: implement BaseHandler to use 
    - Inhert it from your commandhandler class and override the Execute method to contain your code
    - any class that inhert from CommandHandler only accept input that of type ICommand<Data>. the class will then return its result as Result<Data>
   ```csharp 
        public class CreateUserCommand : ICommand<Guid>
        {
            public required string Name { get; set; }
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
   ```
    
3. QueryHandler: implement BaseHandler to use 
    - Inhert it from your Queryhandler class and override the Fetch method to contain your code
    - any class that inhert from Queryhandler only accept input that of type IQuery<Data>. the class will then return its result as Result<Data>
     ```csharp 
      public class GetAdminQuery : IQuery<Guid>
      {
          public string? Name { get; set; }
      }
      ```
  3. QueryHandlerAsync: implement BaseHandler to use 
    - Inhert it from your Queryhandler class and override the Fetch method to contain your code
    - any class that inhert from Queryhandler only accept input that of type IQuery<Data>. the class will then return its result as Result<Data>
     ```csharp 
      public class GetAdminQuery : IQuery<Guid>
      {
          public string? Name { get; set; }
      }
      ```