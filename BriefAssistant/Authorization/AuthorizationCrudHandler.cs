using System.Threading.Tasks;
using BriefAssistant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Authorization
{
    public class BriefAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, IUserData>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BriefAuthorizationCrudHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            IUserData resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if ((requirement == Operations.Create || requirement == Operations.Update ||
                 requirement == Operations.Delete || requirement == Operations.Delete) &&
                resource.ApplicationUserId.ToString() == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}