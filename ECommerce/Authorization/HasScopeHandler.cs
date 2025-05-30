﻿using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Authorization
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasScopeRequirement requirement)
        {
           // if user does not have the scope claim, get out of here
           if(!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // split the scopes string into an array
            var scopes = context!.User
                .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)
                    .Value.Split(" ");

            // succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
