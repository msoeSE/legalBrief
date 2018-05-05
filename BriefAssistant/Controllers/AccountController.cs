using System;
using System.Net;
using System.Threading.Tasks;
using BriefAssistant.Data;
using BriefAssistant.Filters;
using BriefAssistant.Models;
using BriefAssistant.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BriefAssistant.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IHostingEnvironment env, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _env = env;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest model)
        {
            var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = CreateConfirmEmailUri(user.Id.ToString(), code);
                var templateFIlename =
                    _env.ContentRootFileProvider.GetFileInfo("EmailTemplates/registrationTemplate.cshtml").PhysicalPath;
                const string subject = "Please Confirm Your Email";
                await _emailSender.SendEmailAsync(user.Email, subject, templateFIlename, new {Url = callbackUrl});

                if (model.UserType == UserType.Lawyer)
                {
                    await _userManager.AddToRoleAsync(user, "Lawyer");
                }else if (model.UserType == UserType.User)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                    

                return NoContent();
            }
            AddErrors(result);

            return BadRequest(ModelState);
        }

        private Uri CreateConfirmEmailUri(string userId, string code)
        {
            var builder = new UriBuilder()
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Path = "account/confirmation",
                Query = $"userId={WebUtility.UrlEncode(userId)}&code={WebUtility.UrlEncode(code)}"
            };
            if (Request.Host.Port.HasValue)
            {
                builder.Port = Request.Host.Port.Value;
            }

            return builder.Uri;
        }

        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> ForgotPassword([FromBody]EmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                // Don't reveal that the user does not exist or is not confirmed to prevent user enumeration
                return NoContent();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = CreateForgotPasswordUri(user.Email, code);
            var templateFIlename =
                _env.ContentRootFileProvider.GetFileInfo("EmailTemplates/forgotPasswordTemplate.cshtml").PhysicalPath;
            const string subject = "Reset Your Password";
            await _emailSender.SendEmailAsync(user.Email, subject, templateFIlename, new { Url = url });
            return NoContent();

        }

        private Uri CreateForgotPasswordUri(string email, string code)
        {
            var builder = new UriBuilder()
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Path = "account/reset-password",
                Query = $"email={WebUtility.UrlEncode(email)}&code={WebUtility.UrlEncode(code)}"
            };
            if (Request.Host.Port.HasValue)
            {
                builder.Port = Request.Host.Port.Value;
            }

            return builder.Uri;
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist to prevent user enumeration
                return Unauthorized();
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);
            if (result.Succeeded)
            {
                return NoContent();
            }

            AddErrors(result);
            return Unauthorized();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return NoContent();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
