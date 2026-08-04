namespace WorldAlerts.Api.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Authorization filter attribute that protects endpoints by requiring a valid admin identifier in the query string.
/// This is a simple MVP-level authorization mechanism and should be replaced with proper authentication/authorization in production.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SimpleAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    private const string AdminIdentifier = "admin";
    private const string QueryParameterName = "adminKey";

    /// <summary>
    /// Executes the authorization filter.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var providedKey = context.HttpContext.Request.Query[QueryParameterName].ToString();

        if (string.IsNullOrEmpty(providedKey) || providedKey != AdminIdentifier)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
