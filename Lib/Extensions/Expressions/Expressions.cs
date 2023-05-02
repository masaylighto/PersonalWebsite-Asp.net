using System.Linq.Expressions;
namespace Expressions;
class RebindParameterVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;
    public RebindParameterVisitor(ParameterExpression parameter) => _parameter = parameter;
    protected override Expression VisitParameter(ParameterExpression node) => _parameter;
}
public static class ExpressionsExtensions
{

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> sourceExpression, Expression<Func<T, bool>> additionalExpression)
    {
        Expression rewrittenExpressionBody = additionalExpression.RebindBodyParamFrom(sourceExpression);
        return LambdaFrom<T>(Expression.OrElse(sourceExpression.Body, rewrittenExpressionBody), sourceExpression.Parameters[0]);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> sourceExpression, Expression<Func<T, bool>> additionalExpression)
    {
        Expression rewrittenExpressionBody = additionalExpression.RebindBodyParamFrom(sourceExpression);
        return LambdaFrom<T>(Expression.AndAlso(sourceExpression.Body, rewrittenExpressionBody), sourceExpression.Parameters[0]);
    }
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> sourceExpression)
    {
        return Expression.Lambda<Func<T, bool>>(Expression.Not(sourceExpression.Body), sourceExpression.Parameters);
    }


    private static Expression<Func<T, bool>> LambdaFrom<T>(Expression body, params ParameterExpression[]? parameters)
    {
        return Expression.Lambda<Func<T, bool>>(body, parameters);
    }

    /// <summary>
    /// Expressions differ in the name of the function parameter used inside of them,
    /// if we didn't rebind the parameter when we combine the expressions,
    /// that will result that the expression body will use a parameter name differ than the one passed in the function,
    /// which will lead the expression to fail to be of use and will throw exception.
    /// </summary>
    /// <typeparam name="T">Expression Parameter</typeparam>
    /// <returns></returns>
    private static Expression RebindBodyParamFrom<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        return new RebindParameterVisitor(expr2.Parameters[0]).Visit(expr1.Body);
    }
}