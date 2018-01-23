using System;
using System.ComponentModel;
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
using OpenXmlPowerTools;

namespace BriefAssistant.Controllers
{
    [Route("api/[controller]")]
    public class BriefsController : Controller
    {
        private const string DocxMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private readonly IHostingEnvironment _env;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public BriefsController(IHostingEnvironment env, IEmailSender emailSender, ApplicationDbContext applicationContext, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _env = env;
            _emailSender = emailSender;
            _applicationContext = applicationContext;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Creates a new brief
        /// </summary>
        /// <param name="briefInfo"></param>
        /// <returns>
        /// 201 if sucessfully created
        /// 400 if the reqest body is maliformed
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] BriefInfo briefInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var briefDto = Mapper.Map<BriefDto>(briefInfo);
            briefDto.ApplicationUserId = currentUser.Id;
            briefDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            briefDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            _applicationContext.Briefs.Add(briefDto);

            await _applicationContext.SaveChangesAsync();

            return Created($"/briefs/{briefDto.Id}", Json(briefInfo));
        }

        /// <summary>
        /// Updates an existing brief
        /// </summary>
        /// <param name="id"></param>
        /// <param name="briefInfo"></param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] BriefInfo briefInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBrief = await _applicationContext.Briefs
                .Include(briefDto => briefDto.ContactInfoDto)
                .Include(briefDto => briefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.Id == briefInfo.Id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var updatedBrief = Mapper.Map<BriefDto>(briefInfo);
            updatedBrief.Id = existingBrief.Id;
            updatedBrief.ApplicationUserId = existingBrief.ApplicationUserId;
            updatedBrief.ContactInfoDto.Id = existingBrief.ContactInfoDto.Id;
            updatedBrief.ContactInfoDto.ApplicationUserId = existingBrief.ContactInfoDto.ApplicationUserId;
            updatedBrief.CircuitCourtCaseDto.Id = existingBrief.CircuitCourtCaseDto.Id;
            updatedBrief.CircuitCourtCaseDto.ApplicationUserId = existingBrief.CircuitCourtCaseDto.ApplicationUserId;

            return Json(updatedBrief);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBriefsAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Forbid();
            }

            _applicationContext.Entry(currentUser)
                .Collection(t => t.Briefs)
                .Load();

            var result = new BriefList
            {
                Briefs = currentUser.Briefs.Select(Mapper.Map<BriefListItem>)
            };

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBriefAsync(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            var dto = await _applicationContext.Briefs.FindAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            
            var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);
            var briefInfo = Mapper.Map<BriefInfo>(dto);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Json(briefInfo);
        }


        private bool WriteDocumentToStream(BriefInfo value, Stream outputStream)
        {
            value.AppellateCase = new AppellateCase
            {
                District = GetDistrictFromCounty(value.CircuitCourtCase.County)
            };

            value.Date = DateTime.Now.ToShortDateString();

            value.CircuitCourtCase.OpponentRole = GetOpponentRole(value.CircuitCourtCase.Role);

            XElement data;
            using (var dataStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(BriefInfo));
                using (var writer = XmlDictionaryWriter.CreateTextWriter(dataStream, Encoding.UTF8, false))
                {
                    serializer.WriteObject(writer, value);
                }

                dataStream.Position = 0;
                data = XElement.Load(dataStream);
            }

            var templateDoc = new WmlDocument(_env.ContentRootFileProvider.GetFileInfo("briefTemplate.docx").PhysicalPath);
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            assembledDoc.WriteByteArray(outputStream);
            return !isTemplateError;
        }

        private Role GetOpponentRole(Role role)
        {
            switch (role)
            {
                case Role.Plaintiff:
                    return Role.Defendent;
                case Role.Defendent:
                case Role.Petitioner:
                    return Role.Respondent;
                case Role.Respondent:
                    return Role.Petitioner;
                default:
                    throw new InvalidEnumArgumentException(nameof(role), (int)role, typeof(Role));
            }
            
        }

        private District GetDistrictFromCounty(County county)
        {
            switch (county)
            {
                case County.Milwaukee:
                    return District.One;
                case County.Calumet:
                case County.FondDuLac:
                case County.GreenLake:
                case County.Kenosha:
                case County.Manitowoc:
                case County.Ozaukee:
                case County.Racine:
                case County.Sheboygan:
                case County.Walworth:
                case County.Washington:
                case County.Waukesha:
                case County.Winnebago:
                    return District.Two;
                case County.Ashland:
                case County.Barron:
                case County.Bayfield:
                case County.Brown:
                case County.Buffalo:
                case County.Burnett:
                case County.Chippewa:
                case County.Door:
                case County.Douglas:
                case County.Dunn:
                case County.EauClaire:
                case County.Florence:
                case County.Forest:
                case County.Iron:
                case County.Kewaunee:
                case County.Langlade:
                case County.Lincoln:
                case County.Marathon:
                case County.Marinette:
                case County.Menominee:
                case County.Oconto:
                case County.Oneida:
                case County.Outagamie:
                case County.Pepin:
                case County.Pierce:
                case County.Polk:
                case County.Price:
                case County.Rusk:
                case County.Sawyer:
                case County.Shawano:
                case County.StCroix:
                case County.Taylor:
                case County.Trempealeau:
                case County.Vilas:
                case County.Washburn:
                    return District.Three;
                case County.Adams:
                case County.Clark:
                case County.Columbia:
                case County.Crawford:
                case County.Dane:
                case County.Dodge:
                case County.Grant:
                case County.Green:
                case County.Iowa:
                case County.Jackson:
                case County.Jefferson:
                case County.Juneau:
                case County.LaCrosse:
                case County.Lafayette:
                case County.Marquette:
                case County.Monroe:
                case County.Portage:
                case County.Richland:
                case County.Rock:
                case County.Sauk:
                case County.Vernon:
                case County.Waupaca:
                case County.Waushara:
                case County.Wood:
                    return District.Four;
                default:
                    throw new InvalidEnumArgumentException(nameof(county), (int)county, typeof(County));
            }
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadBrief(Guid id)
        {
            var briefDto = await _applicationContext.Briefs.FindAsync(id);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, briefDto, Operations.Read);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<BriefInfo>(briefDto);
            using (var stream = new MemoryStream())
            {
                WriteDocumentToStream(briefInfo, stream);
                return File(stream, DocxMimeType, briefInfo.Title + ".docx");
            }
        }

        [HttpPost("{id}/email")]
        public async Task<IActionResult> EmailBrief(string id, [FromBody] EmailRequest request)
        {
            if (ModelState.IsValid)
            {
                var dto = await _applicationContext.Briefs.FindAsync(id);
                var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                var brief = Mapper.Map<BriefInfo>(dto);
                using (var stream = new MemoryStream())
                {
                    WriteDocumentToStream(brief, stream);
                    await _emailSender.SendBriefAsync(request.Email, stream, brief.Title + ".docx");
                }
                return NoContent();
            }


            return NotFound();
        }
    }
}