using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ICollection<Brief> briefs = currentuser.BriefRecord;
            //check if the brief already exist, if yes, update the tables, if not, insert records to the tables
            if (briefs.Any(i =>i.Name == briefInfo.BriefName))
            {
                Brief brief = briefs.FirstOrDefault(i => i.Name == briefInfo.BriefName);

                brief.UserInfo.Phone = briefInfo.Appellant.Phone;
                brief.UserInfo.State = briefInfo.Appellant.Address.State;
                brief.UserInfo.Street = briefInfo.Appellant.Address.Street;
                brief.UserInfo.Street2 = briefInfo.Appellant.Address.Street2;
                brief.UserInfo.Zip = briefInfo.Appellant.Address.Zip;
                brief.UserInfo.City = briefInfo.Appellant.Address.City;
                              
                _context.SaveChanges();

                brief.CaseInfo.CaseNumber = briefInfo.CircuitCourtCase.CircuitCourtCaseNumber;
                brief.CaseInfo.County = briefInfo.CircuitCourtCase.County;
                brief.CaseInfo.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
                brief.CaseInfo.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
                brief.CaseInfo.OpponentName = briefInfo.CircuitCourtCase.OpponentName;
                brief.CaseInfo.Role = briefInfo.CircuitCourtCase.Role;
                _context.SaveChanges();
            

                brief.BriefInfo.AppendixDocuments = briefInfo.AppendixDocuments;
                brief.BriefInfo.AppellateCourtCaseNumber = briefInfo.AppellateCourtCaseNumber;
                brief.BriefInfo.Argument = briefInfo.Argument;
                brief.BriefInfo.CaseFactsStatement = briefInfo.CaseFactsStatement;
                brief.BriefInfo.Conclusion = briefInfo.Conclusion;
                brief.BriefInfo.IssuesPresented = briefInfo.IssuesPresented;
                brief.BriefInfo.OralArgumentStatement = briefInfo.OralArgumentStatement;
                brief.BriefInfo.PublicationStatement = briefInfo.PublicationStatement;
                _context.SaveChanges();
            }
            else {
                DbUserInfo user = new DbUserInfo
                {
                    Name = briefInfo.Appellant.Name,
                    Phone = briefInfo.Appellant.Phone,
                    State = briefInfo.Appellant.Address.State,
                    Street = briefInfo.Appellant.Address.Street,
                    Street2 = briefInfo.Appellant.Address.Street2,
                    Zip = briefInfo.Appellant.Address.Zip,
                    City = briefInfo.Appellant.Address.City,
                };

                _context.UserInfo.Add(user);
                _context.SaveChanges();


                DbCaseInfo circuitCourtCase = new DbCaseInfo
                {
                    CaseNumber = briefInfo.CircuitCourtCase.CircuitCourtCaseNumber,
                    County = briefInfo.CircuitCourtCase.County,
                    JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName,
                    JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName,
                    OpponentName = briefInfo.CircuitCourtCase.OpponentName,
                    Role = briefInfo.CircuitCourtCase.Role,

                };
                _context.CaseInfo.Add(circuitCourtCase);
                _context.SaveChanges();
                var caseId = circuitCourtCase.CaseId;


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
                Brief brief = new Brief
                {
                    Name = briefInfo.BriefName,
                    BriefInfo = dbbriefInfo,
                    CaseInfo = circuitCourtCase,
                    UserInfo = user

                    
                };
                _context.Brief.Add(brief);
                currentuser.BriefRecord.Add(brief);
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

            ICollection<Brief> briefs = currentuser.BriefRecord;

            Brief brief = briefs.FirstOrDefault(i => i.Name == briefName);


            var user = brief.UserInfo;

            var caseInfo = brief.CaseInfo;

            var briefInfo = brief.BriefInfo;
                Address address = new Address
                {
                    City = user.City,
                    State = user.State,
                    Street = user.Street,
                    Street2 = user.Street2,
                    Zip = user.Zip

                };
                Appellant appellant = new Appellant
                {
                    Address = address,
                    Phone = user.Phone,
                    Name = user.Name,
                    Email = user.Email,

                };
                CircuitCourtCase circuitCourtCase = new CircuitCourtCase
                {
                    CircuitCourtCaseNumber = caseInfo.CaseNumber,
                    County = caseInfo.County,
                    JudgeFirstName = caseInfo.JudgeFirstName,
                    JudgeLastName = caseInfo.JudgeLastName,
                    OpponentName = caseInfo.OpponentName,
                    Role = caseInfo.Role,

                };
                BriefInfo info = new BriefInfo
                {
                    Appellant = appellant,
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