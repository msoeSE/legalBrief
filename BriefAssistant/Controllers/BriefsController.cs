﻿using System;
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
        /// Creates a new brief database object
        /// </summary>
        /// <param name="briefInfo">
        /// The information used to create a brief
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
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
        /// Creates a new initial brief database object
        /// </summary>
        /// <param name="briefHolder">
        /// The object that holds the necessary brief and initialBrief data
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
        /// </returns>
        [HttpPost("initialcreate")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] InitialBriefHolder briefHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var briefInfoDto = Mapper.Map<BriefDto>(briefHolder.BriefInfo);
            briefInfoDto.ApplicationUserId = currentUser.Id;
            briefInfoDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            briefInfoDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Briefs.AddAsync(briefInfoDto);

            await _applicationContext.SaveChangesAsync();

            briefHolder.BriefInfo.Id = briefInfoDto.Id;
            briefHolder.InitialBriefInfo.Id = briefInfoDto.Id;

            var existingBrief = await _applicationContext.Briefs
                .Include(briefDto => briefDto.ContactInfoDto)
                .Include(briefDto => briefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.Id == briefInfoDto.Id);

            var initialBriefDto = Mapper.Map<InitialBriefDto>(briefHolder.InitialBriefInfo);
            initialBriefDto.ApplicationUserId = currentUser.Id;
            initialBriefDto.Id = existingBrief.Id;
            await _applicationContext.Initials.AddAsync(initialBriefDto);

            await _applicationContext.SaveChangesAsync();

            return Created($"/briefs/{initialBriefDto.Id}", briefHolder);
        }

        /// <summary>
        /// Creates a new reply brief database object
        /// </summary>
        /// <param name="briefHolder">
        /// The object that holds the necessary brief and replyBrief data
        /// </param>
        /// <returns>
        /// 201 if successfully created
        /// 400 if the request body is malformed
        /// </returns>
        [HttpPost("replycreate")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReplyBriefHolder briefHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var briefInfoDto = Mapper.Map<BriefDto>(briefHolder.BriefInfo);
            briefInfoDto.ApplicationUserId = currentUser.Id;
            briefInfoDto.CircuitCourtCaseDto.ApplicationUserId = currentUser.Id;
            briefInfoDto.ContactInfoDto.ApplicationUserId = currentUser.Id;
            await _applicationContext.Briefs.AddAsync(briefInfoDto);

            await _applicationContext.SaveChangesAsync();

            briefHolder.BriefInfo.Id = briefInfoDto.Id;
            briefHolder.ReplyBriefInfo.Id = briefInfoDto.Id;

            var existingBrief = await _applicationContext.Briefs
                .Include(briefDto => briefDto.ContactInfoDto)
                .Include(briefDto => briefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.Id == briefInfoDto.Id);

            var replyBriefDto = Mapper.Map<ReplyBriefDto>(briefHolder.ReplyBriefInfo);
            replyBriefDto.ApplicationUserId = currentUser.Id;
            replyBriefDto.Id = existingBrief.Id;
            await _applicationContext.Replys.AddAsync(replyBriefDto);

            await _applicationContext.SaveChangesAsync();

            return Created($"/briefs/{replyBriefDto.Id}", briefHolder);
        }

        //TODO add ResponseCreate
        //TODO add PetitionCreate

        /// <summary>
        /// Updates an existing brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefInfo">
        /// The new information that the brief is updated with
        /// </param>
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
                .SingleAsync(briefDto => briefDto.Id == id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            updateExistingBrief(existingBrief, briefInfo);

            await _applicationContext.SaveChangesAsync();

            return Json(briefInfo);
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
        private void updateExistingBrief(BriefDto existingBrief, BriefInfo briefInfo)
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

            existingBrief.CircuitCourtCaseDto.County = briefInfo.CircuitCourtCase.County;
            existingBrief.CircuitCourtCaseDto.CaseNumber = briefInfo.CircuitCourtCase.CaseNumber;
            existingBrief.CircuitCourtCaseDto.Role = briefInfo.CircuitCourtCase.Role;
            existingBrief.CircuitCourtCaseDto.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
            existingBrief.CircuitCourtCaseDto.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
            existingBrief.CircuitCourtCaseDto.OpponentName = briefInfo.CircuitCourtCase.OpponentName;
        }

        /// <summary>
        /// Updates an existing initial brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefHolder">
        /// The object that holds the new brief and initialBrief information
        /// </param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("initialupdate/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] InitialBriefHolder briefHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var briefInfo = briefHolder.BriefInfo;
            var initialInfo = briefHolder.InitialBriefInfo;

            var existingBrief = await _applicationContext.Briefs
                .Include(briefDto => briefDto.ContactInfoDto)
                .Include(briefDto => briefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.Id == id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            updateExistingBrief(existingBrief, briefInfo);

            var existingInitialBrief = await _applicationContext.Initials
                .SingleAsync(briefDto => briefDto.Id == id);

            if (existingInitialBrief == null)
            {
                return NotFound();
            }

            authResult = await _authorizationService.AuthorizeAsync(User, existingInitialBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            existingInitialBrief.IssuesPresented = initialInfo.IssuesPresented;
            existingInitialBrief.OralArgumentStatement = initialInfo.OralArgumentStatement;
            existingInitialBrief.PublicationStatement = initialInfo.PublicationStatement;
            existingInitialBrief.CaseFactsStatement = initialInfo.CaseFactsStatement;
            existingInitialBrief.AppendixDocuments = initialInfo.AppendixDocuments;

            await _applicationContext.SaveChangesAsync();

            return Json(briefHolder);
        }

        /// <summary>
        /// Updates an existing reply brief database object
        /// </summary>
        /// <param name="id">
        /// The id of the brief being updated
        /// </param>
        /// <param name="briefHolder">
        /// The object that holds the new brief and replyBrief information
        /// </param>
        /// <returns>
        /// 200 if the brief is updated successfully
        /// 400 if the request is not in the correct format
        /// 403 if the user id of the brief does not match the user id of the currently logged in user
        /// 404 if there is no existing brief with the given id
        /// </returns>
        [HttpPut("replyupdate/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReplyBriefHolder briefHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var briefInfo = briefHolder.BriefInfo;
            var replyInfo = briefHolder.ReplyBriefInfo;

            var existingBrief = await _applicationContext.Briefs
                .Include(briefDto => briefDto.ContactInfoDto)
                .Include(briefDto => briefDto.CircuitCourtCaseDto)
                .SingleAsync(briefDto => briefDto.Id == id);

            if (existingBrief == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, existingBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            updateExistingBrief(existingBrief, briefInfo);

            var existingReplyBrief = await _applicationContext.Replys
                .SingleAsync(briefDto => briefDto.Id == id);

            if (existingReplyBrief == null)
            {
                return NotFound();
            }

            authResult = await _authorizationService.AuthorizeAsync(User, existingReplyBrief, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            //TODO existingReplyBrief.variable = replyInfo.variable; if such variables exist in the future

            await _applicationContext.SaveChangesAsync();

            return Json(briefHolder);
        }

        //TODO add ResponseUpdate
        //TODO add PetitionUpdate

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
            var briefInfo = Mapper.Map<BriefInfo>(dto);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

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
            var briefInfo = Mapper.Map<InitialBriefInfo>(dto);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

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
        [HttpGet("replys/{id}")]
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
            var briefInfo = Mapper.Map<ReplyBriefInfo>(dto);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Json(briefInfo);
        }

        //TODO add GetResponseBriefAsync
        //TODO add GetPetitionBriefAsync

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
                .SingleAsync(brief => brief.Id == id);
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
                .SingleAsync(brief => brief.Id == id);
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
            return await _applicationContext.Replys
                .SingleAsync(brief => brief.Id == id);
        }

        //TODO add FindResponseBriefAsync
        //TODO add FindPetitionBriefAsync

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
            if (briefInfo.Type == BriefType.Initial)
            {
                templateName = "initialBriefTemplate.docx";
                InitialBriefDto init = await FindInitialBriefAsync(briefInfo.Id);
                if (init != null)
                {
                    //var authResult = await _authorizationService.AuthorizeAsync(User, init, Operations.Read);
                    var info = Mapper.Map<InitialBriefInfo>(init);
                    exportData.SetInitialInformation(info);
                }
            }
            else if (briefInfo.Type == BriefType.Reply)
            {
                templateName = "replyBriefTemplate.docx";
                ReplyBriefDto reply = await FindReplyBriefAsync(briefInfo.Id);
                if (reply != null)
                {
                    //var authResult = await _authorizationService.AuthorizeAsync(User, reply, Operations.Read);
                    var info = Mapper.Map<ReplyBriefInfo>(reply);
                    exportData.SetReplyInformation(info);
                }

            }
            else if (briefInfo.Type == BriefType.Response)
            {
                templateName = "responseBriefTemplate.docx";
                //TODO
            }
            else if (briefInfo.Type == BriefType.Petition)
            {
                templateName = "petitionForReviewTemplate.docx";
                //TODO
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
                    await WriteDocumentToStream(brief, stream);
                    await _emailSender.SendBriefAsync(request.Email, stream, (brief.Title ?? "brief") + ".docx");
                }
                return NoContent();
            }

            return NotFound();
        }
    }
}