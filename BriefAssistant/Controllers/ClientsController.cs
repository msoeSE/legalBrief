using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AutoMapper;
using BriefAssistant.Authorization;
using BriefAssistant.Data;
using BriefAssistant.Extensions;
using BriefAssistant.Models;
using BriefAssistant.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BriefAssistant.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ApplicationDbContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ClientsController(IHostingEnvironment env, ApplicationDbContext applicationContext, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _env = env;
            _applicationContext = applicationContext;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpPost("createClient")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ContactInfo clientInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var clientInfoDto = Mapper.Map<ContactInfoDto>(clientInfo);
            clientInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Contacts.AddAsync(clientInfoDto);
            await _applicationContext.SaveChangesAsync();
            var clientId = clientInfoDto.Id;


            return Created($"/clients/{clientId}", clientInfo);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetContactsAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Forbid();
            }

            _applicationContext.Entry(currentUser)
                .Collection(t => t.Contacts)
                .Load();

            var result = new ContactList
            {
                Contacts = currentUser.Contacts.Select(Mapper.Map<ContactInfo>)
            };

            return Json(result);
        }
    }
}
