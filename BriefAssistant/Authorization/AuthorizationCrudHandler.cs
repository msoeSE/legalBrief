using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using BriefAssistant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Authorization
{
    public class BriefAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, IUserData>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            IUserData resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if ((requirement == Operations.Create || requirement == Operations.Update ||
                 requirement == Operations.Read || requirement == Operations.Delete) &&
                resource.ApplicationUserId.ToString() == context.User.FindFirstValue(OpenIdConnectConstants.Claims.Subject))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}