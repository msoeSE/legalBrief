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
using BriefAssistant.Filters;
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
        /// Creates a new initial brief database object
        /// </summary>
        /// <param name="briefInfo">
        /// The object that holds the necessary initialBrief data
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
        /// </returns>
        [HttpPost("initialcreate")]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] InitialBriefInfo briefInfo)
        {
            if (briefInfo.BriefInfo.Type != BriefType.Initial)
            {
                return BadRequest("Brief Type mismatch");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var initialBriefDto = Mapper.Map<InitialBriefDto>(briefInfo);
            initialBriefDto.ApplicationUserId = currentUser.Id;
            initialBriefDto.BriefDto.ApplicationUserId = currentUser.Id;
            initialBriefDto.BriefDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            initialBriefDto.BriefDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Initials.AddAsync(initialBriefDto);
            await _applicationContext.SaveChangesAsync();

            briefInfo.Id = initialBriefDto.BriefId;
            briefInfo.BriefInfo.Id = initialBriefDto.BriefId;

            return Created($"/briefs/{initialBriefDto.BriefId}", briefInfo);
        }

        /// <summary>
        /// Creates a new reply brief database object
        /// </summary>
        /// <param name="briefInfo">
        /// The object that holds the necessary replyBrief data
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
        /// </returns>
        [HttpPost("replycreate")]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] ReplyBriefInfo briefInfo)
        {
            if (briefInfo.BriefInfo.Type != BriefType.Reply)
            {
                return BadRequest("Brief type mismatch");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var replyBriefDto = Mapper.Map<ReplyBriefDto>(briefInfo);
            replyBriefDto.ApplicationUserId = currentUser.Id;
            replyBriefDto.BriefDto.ApplicationUserId = currentUser.Id;
            replyBriefDto.BriefDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            replyBriefDto.BriefDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Replies.AddAsync(replyBriefDto);
            await _applicationContext.SaveChangesAsync();

            briefInfo.Id = replyBriefDto.BriefId;
            briefInfo.BriefInfo.Id = replyBriefDto.BriefId;

            return Created($"/briefs/{briefInfo.Id}", briefInfo);
        }

        /// <summary>
        /// Creates a new response brief database object
        /// </summary>
        /// <param name="briefInfo">
        /// The object that holds the necessary responseBrief data
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
        /// </returns>
        [HttpPost("responsecreate")]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] ResponseBriefInfo briefInfo)
        {
            if (briefInfo.BriefInfo.Type != BriefType.Response)
            {
                return BadRequest("Brief type mismatch");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var responseBriefDto = Mapper.Map<ResponseBriefDto>(briefInfo);
            responseBriefDto.ApplicationUserId = currentUser.Id;
            responseBriefDto.BriefDto.ApplicationUserId = currentUser.Id;
            responseBriefDto.BriefDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            responseBriefDto.BriefDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Responses.AddAsync(responseBriefDto);
            await _applicationContext.SaveChangesAsync();

            briefInfo.Id = responseBriefDto.BriefId;
            briefInfo.BriefInfo.Id = responseBriefDto.BriefId;

            return Created($"/briefs/{responseBriefDto.BriefId}", briefInfo);
        }

        /// <summary>
        /// Updates an existing brief database object with new brief information
        /// </summary>
        /// <param name="existingBrief">
        /// The brief database object being updated
        /// </param>
        /// <param name="briefInfo">
        /// The new information that the brief is updated with
        /// </param>
        private static void UpdateExistingBrief(BriefDto existingBrief, BriefInfo briefInfo)
        {
            existingBrief.Title = briefInfo.Title;
            existingBrief.AppellateCourtCaseNumber = briefInfo.AppellateCourtCaseNumber;
            existingBrief.Argument = briefInfo.Argument;
            existingBrief.Conclusion = briefInfo.Conclusion;

            existingBrief.ContactInfoDto.Name = briefInfo.ContactInfo.Name;
            existingBrief.ContactInfoDto.Street = briefInfo.ContactInfo.Address.Street;
            existingBrief.ContactInfoDto.Street2 = briefInfo.ContactInfo.Address.Street2;
            existingBrief.ContactInfoDto.City = briefInfo.ContactInfo.Address.City;
            existingBrief.ContactInfoDto.State = briefInfo.ContactInfo.Address.State;
            existingBrief.ContactInfoDto.Zip = briefInfo.ContactInfo.Address.Zip;
            existingBrief.ContactInfoDto.Email = briefInfo.ContactInfo.Email;
            existingBrief.ContactInfoDto.Phone = briefInfo.ContactInfo.Phone;
            existingBrief.ContactInfoDto.BarId = briefInfo.ContactInfo.BarId;

            existingBrief.CircuitCourtCaseDto.County = briefInfo.CircuitCourtCase.County;
            existingBrief.CircuitCourtCaseDto.CaseNumber = briefInfo.CircuitCourtCase.CaseNumber;
            existingBrief.CircuitCourtCaseDto.Role = briefInfo.CircuitCourtCase.Role;
            existingBrief.CircuitCourtCaseDto.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
            existingBrief.CircuitCourtCaseDto.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
            existingBrief.CircuitCourtCaseDto.OpponentName = briefInfo.CircuitCourtCase.OpponentName;
            existingBrief.CircuitCourtCaseDto.ClientName = briefInfo.CircuitCourtCase.ClientName;
        }

        /// <summary>
        /// Updates an existing initial brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefInfo">
        /// The object that holds the new initialBrief information
        /// </param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("initialupdate/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] InitialBriefInfo briefInfo)
        {
            var existingBrief = await FindInitialBriefAsync(id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            UpdateExistingBrief(existingBrief.BriefDto, briefInfo.BriefInfo);

            existingBrief.IssuesPresented = briefInfo.IssuesPresented;
            existingBrief.OralArgumentStatement = briefInfo.OralArgumentStatement;
            existingBrief.PublicationStatement = briefInfo.PublicationStatement;
            existingBrief.CaseFactsStatement = briefInfo.CaseFactsStatement;
            existingBrief.AppendixDocuments = briefInfo.AppendixDocuments;

            await _applicationContext.SaveChangesAsync();

            return Json(briefInfo);
        }

        /// <summary>
        /// Updates an existing reply brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefInfo">
        /// The object that holds the new replyBrief information
        /// </param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("replyupdate/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReplyBriefInfo briefInfo)
        {
            var existingBrief = await FindReplyBriefAsync(id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            UpdateExistingBrief(existingBrief.BriefDto, briefInfo.BriefInfo);

            await _applicationContext.SaveChangesAsync();

            return Json(briefInfo);
        }

        /// <summary>
        /// Updates an existing response brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefInfo">
        /// The object that holds the new responseBrief information
        /// </param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("responseupdate/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ResponseBriefInfo briefInfo)
        {
            var existingBrief = await FindResponseBriefAsync(id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            UpdateExistingBrief(existingBrief.BriefDto, briefInfo.BriefInfo);

            existingBrief.IssuesPresented = briefInfo.IssuesPresented;
            existingBrief.OralArgumentStatement = briefInfo.OralArgumentStatement;
            existingBrief.PublicationStatement = briefInfo.PublicationStatement;
            existingBrief.CaseFactsStatement = briefInfo.CaseFactsStatement;

            await _applicationContext.SaveChangesAsync();

            return Json(briefInfo);
        }

        /// <summary>
        /// Retrieves the list of briefs created by the currently logged in user
        /// </summary>
        /// <returns>
        /// A JSON object of the briefs
        /// 403 if the user id of the currently logged in user is null
        /// </returns>
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

        /// <summary>
        /// Retreives a brief with a given ID
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// A JSON object of the brief if the brief is retrieved successfully
        /// 400 if the id of the brief is the default GUID value
        /// 403 if the user id of brief does not match the currently logged in user
        /// 404 if the brief could not be found
        /// </returns>
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
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<BriefInfo>(dto);

            return Json(briefInfo);
        }

        /// <summary>
        /// Retreives an initial brief with a given ID
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// A JSON object of the brief if the brief is retrieved successfully
        /// 400 if the id of the brief is the default GUID value
        /// 403 if the user id of brief does not match the currently logged in user
        /// 404 if the brief could not be found
        /// </returns>
        [HttpGet("initials/{id}")]
        public async Task<IActionResult> GetInitialBriefAsync(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            InitialBriefDto dto = await FindInitialBriefAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<InitialBriefInfo>(dto);

            return Json(briefInfo);
        }

        /// <summary>
        /// Retreives a reply brief with a given ID
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// A JSON object of the brief if the brief is retrieved successfully
        /// 400 if the id of the brief is the default GUID value
        /// 403 if the user id of brief does not match the currently logged in user
        /// 404 if the brief could not be found
        /// </returns>
        [HttpGet("replies/{id}")]
        public async Task<IActionResult> GetReplyBriefAsync(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            ReplyBriefDto dto = await FindReplyBriefAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<ReplyBriefInfo>(dto);

            return Json(briefInfo);
        }

        /// <summary>
        /// Retreives an response brief with a given ID
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// A JSON object of the brief if the brief is retrieved successfully
        /// 400 if the id of the brief is the default GUID value
        /// 403 if the user id of brief does not match the currently logged in user
        /// 404 if the brief could not be found
        /// </returns>
        [HttpGet("responses/{id}")]
        public async Task<IActionResult> GetResponseBriefAsync(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            ResponseBriefDto dto = await FindResponseBriefAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Read);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var briefInfo = Mapper.Map<ResponseBriefInfo>(dto);

            return Json(briefInfo);
        }

        /// <summary>
        /// Retrieves the brief database object with the given id
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// The brief database object associated with the ID
        /// </returns>
        private async Task<BriefDto> FindBriefAsync(Guid id)
        {
            return await _applicationContext.Briefs
                .Include(brief => brief.ContactInfoDto)
                .Include(brief => brief.CircuitCourtCaseDto)
                .SingleOrDefaultAsync(brief => brief.Id == id);
        }

        [HttpPost("{id}/delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid))
            {
                return BadRequest();
            }

            BriefDto dto = await FindBriefAsync(id);

            var authResult = await _authorizationService.AuthorizeAsync(User, dto, Operations.Delete);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (dto.Type == BriefType.Initial)
            {
                InitialBriefDto initialBriefDto = await FindInitialBriefAsync(id);
                _applicationContext.Initials.Remove(initialBriefDto);
            } else if (dto.Type == BriefType.Reply)
            {
                ReplyBriefDto replyBriefDto = await FindReplyBriefAsync(id);
                _applicationContext.Replies.Remove(replyBriefDto);
            } else if (dto.Type == BriefType.Response)
            {
                ResponseBriefDto responseBriefDto = await FindResponseBriefAsync(id);
                _applicationContext.Responses.Remove(responseBriefDto);
            }
            _applicationContext.Briefs.Remove(dto);
            _applicationContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Retrieves the initial brief database object with the given id
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// The brief database object associated with the ID
        /// </returns>
        private async Task<InitialBriefDto> FindInitialBriefAsync(Guid id)
        {
            return await _applicationContext.Initials
                .Include(initialDto => initialDto.BriefDto)
                .Include(initialDto => initialDto.BriefDto.ContactInfoDto)
                .Include(initialDto => initialDto.BriefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.BriefId == id);
        }

        /// <summary>
        /// Retrieves the reply database object with the given id
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// The brief database object associated with the ID
        /// </returns>
        private async Task<ReplyBriefDto> FindReplyBriefAsync(Guid id)
        {
            return await _applicationContext.Replies
                .Include(replyDto => replyDto.BriefDto)
                .Include(replyDto => replyDto.BriefDto.ContactInfoDto)
                .Include(replyDto => replyDto.BriefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.BriefId == id);
        }

        /// <summary>
        /// Retrieves the response brief database object with the given id
        /// </summary>
        /// <param name="id">
        /// The ID of the brief being retrieved
        /// </param>
        /// <returns>
        /// The brief database object associated with the ID
        /// </returns>
        private async Task<ResponseBriefDto> FindResponseBriefAsync(Guid id)
        {
            return await _applicationContext.Responses
                .Include(responseDto => responseDto.BriefDto)
                .Include(responseDto => responseDto.BriefDto.ContactInfoDto)
                .Include(responseDto => responseDto.BriefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.BriefId == id);
        }

        /// <summary>
        /// Writes a given brief to an output stream
        /// </summary>
        /// <param name="briefInfo">
        /// The brief being generated
        /// </param>
        /// <param name="outputStream">
        /// The stream that holds the brief
        /// </param>
        /// <returns>
        /// True if there was no template error
        /// False if there was a template error
        /// </returns>
        private async Task<bool> WriteDocumentToStream(BriefInfo briefInfo, Stream outputStream)
        {
            var exportData = new BriefExport(briefInfo);
            String templateName = await GetTemplateName(briefInfo, exportData);
            
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

            var templateDoc = new WmlDocument(_env.ContentRootFileProvider.GetFileInfo(templateName).PhysicalPath);
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            assembledDoc.WriteByteArray(outputStream);
            outputStream.Position = 0;
            return !isTemplateError;
        }

        /// <summary>
        /// Determines which template to be used to generate a brief
        /// Sets the export data's initial, reply, response, or petition information depending on the type of brief
        /// </summary>
        /// <param name="briefInfo">
        /// The data of the brief being generated
        /// </param>
        /// <param name="exportData">
        /// The data being outputted to the template
        /// </param>
        /// <returns>
        /// A string containing the name of the template being used to generate the brief
        /// </returns>
        private async Task<string> GetTemplateName(BriefInfo briefInfo, BriefExport exportData)
        {
            String templateName = null;
            switch (briefInfo.Type)
            {
                case BriefType.Initial:
                    templateName = User.IsInRole("Lawyer") ? "initialBriefTemplateLawyer.docx" : "initialBriefTemplateUser.docx";
                    InitialBriefDto init = await FindInitialBriefAsync(briefInfo.Id);
                    if (init != null)
                    {
                        var info = Mapper.Map<InitialBriefInfo>(init);
                        exportData.SetInitialInformation(info);
                    }

                    break;
                case BriefType.Reply:
                    templateName = User.IsInRole("Lawyer") ? "replyBriefTemplateLawyer.docx" : "replyBriefTemplateUser.docx";

                    break;
                case BriefType.Response:
                    templateName = User.IsInRole("Lawyer") ? "responseBriefTemplateLawyer.docx" : "responseBriefTemplateUser.docx";
                    ResponseBriefDto resp = await FindResponseBriefAsync(briefInfo.Id);
                    if (resp != null)
                    {
                        var info = Mapper.Map<ResponseBriefInfo>(resp);
                        exportData.SetResponseInformation(info);
                    }

                    break;
            }

            return templateName;
        }

        /// <summary>
        /// Generates a brief file that the user downloads
        /// </summary>
        /// <param name="id">
        /// The id of the brief being generated
        /// </param>
        /// <returns>
        /// The file to be downloaded
        /// </returns>
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
            await WriteDocumentToStream(briefInfo, stream);
            return File(stream, DocxMimeType, (briefInfo.Title ?? "brief") + ".docx");
        }

        /// <summary>
        /// Creates an email containing the brief that will be sent to the user
        /// </summary>
        /// <param name="id">
        /// The id of the brief to be generated and sent
        /// </param>
        /// <param name="request">
        /// The email address that the email will be sent to
        /// </param>
        /// <returns>
        /// 204 if the email was sent successfully
        /// 404 if the EmailRequest format was not correct
        /// </returns>
        [HttpPost("{id}/email")]
        [ValidateModel]
        public async Task<IActionResult> EmailBrief(Guid id, [FromBody] EmailRequest request)
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
                await WriteDocumentToStream(brief, stream);
                const string subject = "Your Completed Brief";
                var templateFilename =
                    _env.ContentRootFileProvider.GetFileInfo("EmailTemplates/completedBriefTemplate.cshtml").PhysicalPath;
                var attachmentFilename = (brief.Title ?? "brief") + ".docx";
                await _emailSender.SendEmailWithAttachmentAsync(request.Email, subject, templateFilename, stream, attachmentFilename);
            }
            return NoContent();
        }
    }
}