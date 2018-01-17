using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BriefAssistant.Controllers;
using BriefAssistant.Data;
using BriefAssistant.Models;
using BriefAssistant.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BriefAssistant.Tests
{
    public class AccountControllerTests
    {
        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        private static Mock<SignInManager<TUser>> MockSignInManager<TUser>(UserManager<TUser> userManager)
            where TUser : class
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            var mgr = new Mock<SignInManager<TUser>>(userManager, contextAccessor.Object, claimsFactory.Object, null, null, null);
            return mgr;
        }

        private static Mock<IUrlHelper> MockUrlHelper(ActionContext context)
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupGet(h => h.ActionContext).Returns(context);
            return urlHelper;
        }

        [Fact]
        public async Task TestUserRegistrationSuccessReturnsHttp201()
        {
            // Arrange
            var userManager = MockUserManager<ApplicationUser>();
            var signInManager = MockSignInManager(userManager.Object);
            var emailSender = new Mock<IEmailSender>();
            var logger = new Mock<ILogger<AccountController>>();

            var request = new RegistrationRequest
            {
                Email = "bob@example.com",
                Password = "abcdef1!G"
            };

            userManager.Setup(mock => mock.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new AccountController(userManager.Object, signInManager.Object, emailSender.Object,
                logger.Object);
            controller.Url = MockUrlHelper(controller.ControllerContext).Object;


            //Act
            var result = await controller.Register(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
