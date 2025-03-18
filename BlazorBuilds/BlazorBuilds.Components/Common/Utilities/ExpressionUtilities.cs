using System.Linq.Expressions;
using System.Reflection;

namespace BlazorBuilds.Components.Common.Utilities;

public static class ExpressionUtilities
{
    public static Func<T, object> CreatePropertyValueGetter<T>(PropertyInfo propertyInfo)
    {
        var parameter       = Expression.Parameter(typeof(T), "x");
        var propertyAccess  = Expression.Property(parameter, propertyInfo);
        var convert         = Expression.Convert(propertyAccess, typeof(object));

        return Expression.Lambda<Func<T, object>>(convert, parameter).Compile();
    }

    public static string GetPropertyName<TData>(Expression<Func<TData, object>> expression)
    {
        MemberExpression memberExpression;

        if (expression.Body is UnaryExpression unaryExpression)
        {
            memberExpression = unaryExpression.Operand as MemberExpression ?? throw new InvalidOperationException("Invalid expression format");
        }
        else
        {
            memberExpression = expression.Body as MemberExpression ?? throw new InvalidOperationException("Invalid expression format");
        }

        return memberExpression.Member.Name;
    }


}
