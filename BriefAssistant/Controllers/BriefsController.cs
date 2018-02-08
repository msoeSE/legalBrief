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
            await _applicationContext.Briefs.AddAsync(briefDto);

            await _applicationContext.SaveChangesAsync();

            briefInfo.Id = briefDto.Id;
            return Created($"/briefs/{briefDto.Id}", briefInfo);
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

            existingBrief.Title = briefInfo.Title;
            existingBrief.AppellateCourtCaseNumber = briefInfo.AppellateCourtCaseNumber;
            existingBrief.IssuesPresented = briefInfo.IssuesPresented;
            existingBrief.OralArgumentStatement = briefInfo.OralArgumentStatement;
            existingBrief.PublicationStatement = briefInfo.PublicationStatement;
            existingBrief.CaseFactsStatement = briefInfo.CaseFactsStatement;
            existingBrief.Argument = briefInfo.Argument;
            existingBrief.Conclusion = briefInfo.Conclusion;
            existingBrief.AppendixDocuments = briefInfo.AppendixDocuments;

            existingBrief.ContactInfoDto.Name = briefInfo.ContactInfo.Name;
            existingBrief.ContactInfoDto.Street = briefInfo.ContactInfo.Address.Street;
            existingBrief.ContactInfoDto.Street2 = briefInfo.ContactInfo.Address.Street2;
            existingBrief.ContactInfoDto.City = briefInfo.ContactInfo.Address.City;
            existingBrief.ContactInfoDto.State = briefInfo.ContactInfo.Address.State;
            existingBrief.ContactInfoDto.Zip = briefInfo.ContactInfo.Address.Zip;
            existingBrief.ContactInfoDto.Email = briefInfo.ContactInfo.Email;
            existingBrief.ContactInfoDto.Phone = briefInfo.ContactInfo.Phone;

            existingBrief.CircuitCourtCaseDto.County = briefInfo.CircuitCourtCase.County;
            existingBrief.CircuitCourtCaseDto.CaseNumber = briefInfo.CircuitCourtCase.CaseNumber;
            existingBrief.CircuitCourtCaseDto.Role = briefInfo.CircuitCourtCase.Role;
            existingBrief.CircuitCourtCaseDto.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
            existingBrief.CircuitCourtCaseDto.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
            existingBrief.CircuitCourtCaseDto.OpponentName = briefInfo.CircuitCourtCase.OpponentName;

            await _applicationContext.SaveChangesAsync();

            return Json(briefInfo);
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

            BriefDto dto = await FindBriefAsync(id);
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

        private async Task<BriefDto> FindBriefAsync(Guid id)
        {
            return await _applicationContext.Briefs
                .Include(brief => brief.ContactInfoDto)
                .Include(brief => brief.CircuitCourtCaseDto)
                .SingleAsync(brief => brief.Id == id);
        }

        private bool WriteDocumentToStream(BriefInfo briefInfo, Stream outputStream)
        {
            var exportData = new BriefExport(briefInfo);

            XElement data;
            using (var dataStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(BriefExport));
                using (var writer = XmlDictionaryWriter.CreateTextWriter(dataStream, Encoding.UTF8, false))
                {
                    serializer.WriteObject(writer, exportData);
                }

                dataStream.Position = 0;
                data = XElement.Load(dataStream);
            }

            var templateDoc = new WmlDocument(_env.ContentRootFileProvider.GetFileInfo("briefTemplate.docx").PhysicalPath);
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            assembledDoc.WriteByteArray(outputStream);
            outputStream.Position = 0;
            return !isTemplateError;
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadBrief(Guid id)
        {
            var briefDto = await FindBriefAsync(id);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, briefDto, Operations.Read);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<BriefInfo>(briefDto);
            var stream = new MemoryStream();
            WriteDocumentToStream(briefInfo, stream);
            return File(stream, DocxMimeType, (briefInfo.Title ?? "brief") + ".docx");
        }

        [HttpPost("{id}/email")]
        public async Task<IActionResult> EmailBrief(Guid id, [FromBody] EmailRequest request)
        {
            if (ModelState.IsValid)
            {
                var dto = await FindBriefAsync(id);
                var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                var brief = Mapper.Map<BriefInfo>(dto);
                using (var stream = new MemoryStream())
                {
                    WriteDocumentToStream(brief, stream);
                    await _emailSender.SendBriefAsync(request.Email, stream, (brief.Title ?? "brief") + ".docx");
                }
                return NoContent();
            }


            return NotFound();
        }
    }
}