using Ardalis.GuardClauses;
using System.Reflection;

namespace Shopster.API.WebApi.Infrastructure;

public static class MethodInfoExtensions
{
    public static bool IsAnonymous(this MethodInfo method)
    {
        char[] invalidChars = ['<', '>'];
        return method.Name.Any(invalidChars.Contains);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static void AnonymousMethod(this IGuardClause guardClause, Delegate input)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        if (input.Method.IsAnonymous())
        {
            throw new ArgumentException("The endpoint name must be specified when using anonymous handlers.");
        }
    }
}
