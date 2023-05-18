# Core.Expressions
Provide Extensions method the modify Expression
# Methods
1. Or:  which take another expression and combine into the original expression applying OR on it
   ```csharp
     Expression<Func<string, bool>> expression = (text) => text.Contains('1');
     expression.Or(text => text.Contains('2'));
   ```
   the result expression to be put in simpler form
   ```csharp
   (text) => text.Contains('1') || text.Contains('2')
    ```
2. And: which take another expression and combine into the original expression applying AND on it
   ```csharp
     Expression<Func<string, bool>> expression = (text) => text.Contains('1');
     expression.And(text => text.Contains('2'));
   ```
   the result expression to be put in simpler form
   ```csharp
   (text) => text.Contains('1') && text.Contains('2')
    ```
3. Not: which take another expression and combine into the original expression applying NOT on it
   ```csharp
     Expression<Func<string, bool>> expression = (text) => text.Contains('1');
     expression.Not();
   ```
   the result expression to be put in simpler form
   ```csharp
   (text) => !text.Contains('1')
    ```
 4. RebindBodyParamFrom: Expressions are runtime compiled and the stored as tree before compiled and for example 
    if we had the following expression,  ```csharp (text) => text.Contains('1'); ``` it consists of the following the parameters and the body.
    If we somehow need to change the name of the parameter used in the body. If for example there we're going to pass our expression to a method that will pass the parameter with different name
    we can use this method to change the parameter in the body, but this method will only return an expression body if we want to full working expression we should call BodyToLambda directly after the method
    ```csharp
    SoftDelete.RebindBodyParamFrom(Type).BodyToLambda()
    ```
    
