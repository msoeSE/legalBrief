using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BriefAssistant.Data;
using BriefAssistant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Controllers
{

    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly Brief_assistantContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataController(Brief_assistantContext context, UserManager<ApplicationUser> userManager)
        {
            _context=context;
            _userManager = userManager;
        }

        
        public String Id { get; set; }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveBriefforUser([FromBody]BriefInfo briefInfo)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var userId = currentuser.Id;
            ICollection<BriefDto> briefs = currentuser.Briefs;
            //check if the brief already exist, if yes, update the tables, if not, insert records to the tables
            if (briefs.Any(i =>i.Name == briefInfo.Name))
            {
                BriefDto briefDto = briefs.FirstOrDefault(i => i.Name == briefInfo.Name);

                briefDto.ContactInfoDto.Phone = briefInfo.ContactInfo.Phone;
                briefDto.ContactInfoDto.State = briefInfo.ContactInfo.Address.State;
                briefDto.ContactInfoDto.Street = briefInfo.ContactInfo.Address.Street;
                briefDto.ContactInfoDto.Street2 = briefInfo.ContactInfo.Address.Street2;
                briefDto.ContactInfoDto.Zip = briefInfo.ContactInfo.Address.Zip;
                briefDto.ContactInfoDto.City = briefInfo.ContactInfo.Address.City;
                              
                _context.SaveChanges();

                briefDto.CircuitCourtCircuitCaseInfoDto.CaseNumber = briefInfo.CircuitCourtCase.CaseNumber;
                briefDto.CircuitCourtCircuitCaseInfoDto.County = briefInfo.CircuitCourtCase.County;
                briefDto.CircuitCourtCircuitCaseInfoDto.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
                briefDto.CircuitCourtCircuitCaseInfoDto.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
                briefDto.CircuitCourtCircuitCaseInfoDto.OpponentName = briefInfo.CircuitCourtCase.OpponentName;
                briefDto.CircuitCourtCircuitCaseInfoDto.Role = briefInfo.CircuitCourtCase.Role;
                _context.SaveChanges();
            

                briefDto.BriefInfo.AppendixDocuments = briefInfo.AppendixDocuments;
                briefDto.BriefInfo.AppellateCourtCaseNumber = briefInfo.AppellateCourtCaseNumber;
                briefDto.BriefInfo.Argument = briefInfo.Argument;
                briefDto.BriefInfo.CaseFactsStatement = briefInfo.CaseFactsStatement;
                briefDto.BriefInfo.Conclusion = briefInfo.Conclusion;
                briefDto.BriefInfo.IssuesPresented = briefInfo.IssuesPresented;
                briefDto.BriefInfo.OralArgumentStatement = briefInfo.OralArgumentStatement;
                briefDto.BriefInfo.PublicationStatement = briefInfo.PublicationStatement;
                _context.SaveChanges();

            }
            else {
                ContactInfoDto user = new ContactInfoDto
                {
                    Name = briefInfo.ContactInfo.Name,
                    Phone = briefInfo.ContactInfo.Phone,
                    State = briefInfo.ContactInfo.Address.State,
                    Street = briefInfo.ContactInfo.Address.Street,
                    Street2 = briefInfo.ContactInfo.Address.Street2,
                    Zip = briefInfo.ContactInfo.Address.Zip,
                    City = briefInfo.ContactInfo.Address.City,
                };

                _context.UserInfo.Add(user);
                _context.SaveChanges();
                var userInfoId = user.Id;

                CaseDto @case = new CaseDto
                {
                    CaseNumber = briefInfo.CircuitCourtCase.CaseNumber,
                    County = briefInfo.CircuitCourtCase.County,
                    JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName,
                    JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName,
                    OpponentName = briefInfo.CircuitCourtCase.OpponentName,
                    Role = briefInfo.CircuitCourtCase.Role,

                };
                _context.CaseInfo.Add(@case);
                _context.SaveChanges();
                var caseId = @case.Id;


               DbbriefInfo dbbriefInfo = new DbbriefInfo
                {

                    AppendixDocuments = briefInfo.AppendixDocuments,
                    Argument = briefInfo.Argument,
                    CaseFactsStatement = briefInfo.CaseFactsStatement,
                    Conclusion = briefInfo.Conclusion,
                    IssuesPresented = briefInfo.IssuesPresented,
                    AppellateCourtCaseNumber= briefInfo.AppellateCourtCaseNumber,
                    OralArgumentStatement = briefInfo.OralArgumentStatement,
                    PublicationStatement = briefInfo.PublicationStatement

                };
                _context.BriefInfo.Add(dbbriefInfo);
                _context.SaveChanges();
                var briefInfoId = dbbriefInfo.InitialBriefInfoId;


                BriefDto briefDto = new BriefDto
                {
                    Name = briefInfo.Name,
                    CaseId = caseId,
                    ContactInfoId = userInfoId,
                    InitialBriefInfoId = briefInfoId

                    
                };
                _context.Brief.Add(briefDto);
                currentuser.Briefs.Add(briefDto);
                _context.SaveChanges();

            }
            return Ok();

        }

        [HttpPost("retrieve")]
        //briefName is the  brief selected to edit by the user 
        public async Task<IActionResult> RetrieveInfo([FromBody]String briefName)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var userId = currentuser.Id;

            ICollection<BriefDto> briefs = currentuser.Briefs;

            BriefDto briefDto = briefs.FirstOrDefault(i => i.Name == briefName);


            var user = briefDto.ContactInfoDto;

            var caseInfo = briefDto.CircuitCourtCircuitCaseInfoDto;

            var briefInfo = briefDto.BriefInfo;
                Address address = new Address
                {
                    City = user.City,
                    State = user.State,
                    Street = user.Street,
                    Street2 = user.Street2,
                    Zip = user.Zip

                };
                ContactInfo contactInfo = new ContactInfo
                {
                    Address = address,
                    Phone = user.Phone,
                    Name = user.Name,
                    Email = user.Email,

                };
                CircuitCourtCase circuitCourtCase = new CircuitCourtCase
                {
                    CaseNumber = caseInfo.CaseNumber,
                    County = caseInfo.County,
                    JudgeFirstName = caseInfo.JudgeFirstName,
                    JudgeLastName = caseInfo.JudgeLastName,
                    OpponentName = caseInfo.OpponentName,
                    Role = caseInfo.Role,

                };
                BriefInfo info = new BriefInfo
                {
                    ContactInfo = contactInfo,
                    CircuitCourtCase = circuitCourtCase,
                    AppendixDocuments = briefInfo.AppendixDocuments,
                    Argument = briefInfo.Argument,
                    CaseFactsStatement = briefInfo.CaseFactsStatement,
                    AppellateCourtCaseNumber = briefInfo.AppellateCourtCaseNumber,
                    Conclusion = briefInfo.Conclusion,
                    IssuesPresented = briefInfo.IssuesPresented,
                    OralArgumentStatement = briefInfo.OralArgumentStatement,
                    PublicationStatement = briefInfo.PublicationStatement
                };

                return Json(info);

            

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}