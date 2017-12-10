using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BriefAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Mvc;


namespace BriefAssistant.Controllers
{
    [Route("api/[controller]")]

    public class DataController : Controller
    {
        private readonly Brief_assistantContext _context;

        public DataController(Brief_assistantContext context) {
            _context=context;
        }
        [HttpPost("Save")]
        public void SaveBrief(String email,BriefInfo briefInfo,CircuitCourtCase circuitCourtCase)
        {

            var userId = _context.UserInfo
                              .Where(b => b.Email == email)             
                              .Select(u => u.UserId)
                              .FirstOrDefault();
            var caseInfo = _context.CaseInfo
                              .Where(b => b.UserId == userId)
                              .FirstOrDefault();
            caseInfo.CaseNumber = circuitCourtCase.CaseNumber;
            caseInfo.County = circuitCourtCase.County;
            caseInfo.JudgeFirstName = circuitCourtCase.JudgeFirstName;
            caseInfo.JudgeLastName = circuitCourtCase.JudgeLastName;
            caseInfo.OpponentName = circuitCourtCase.OpponentName;
            caseInfo.Role = circuitCourtCase.Role;

            _context.Entry(caseInfo).State = EntityState.Modified;
            
            var brief = _context.BriefInfo
                  .Where(b => b.UserId == userId)
                  .FirstOrDefault();
            brief.AppendexDocuments = briefInfo.AppendexDocuments;
            brief.Argument = briefInfo.Argument;
            brief.CaseFactsStatement = briefInfo.CaseFactsStatement;
            brief.Conclusion = briefInfo.Conclusion;
            brief.Date = briefInfo.Date;
            brief.IssuesPresented = briefInfo.IssuesPresented;
            brief.OralArgumentStatement = briefInfo.OralArgumentStatement;
            brief.PublicationStatement = briefInfo.PublicationStatement;
            _context.Entry(brief).State = EntityState.Modified;
            _context.SaveChanges();

        }
        [HttpGet("Retrieve")]
        public IActionResult RetrieveInfo(String email)
        {

            var user = _context.UserInfo
                              .Where(b => b.Email == email)
                              .FirstOrDefault();

            var caseInfo = _context.CaseInfo
                              .Where(b => b.UserId == user.UserId)
                              .FirstOrDefault();
            var address = _context.Address
                                .Where(b => b.UserId == user.UserId)
                                .FirstOrDefault();           
            var brief = _context.BriefInfo
                  .Where(b => b.UserId == user.UserId)
                  .FirstOrDefault();
            BriefInfo info = new BriefInfo
            {
                Appellant = user,
                CircuitCourtCase = caseInfo,
                AppendexDocuments = brief.AppendexDocuments,
                Argument = brief.Argument,
                CaseFactsStatement = brief.CaseFactsStatement,
                Conclusion = brief.Conclusion,
                Date = brief.Date,
                IssuesPresented = brief.IssuesPresented,
                OralArgumentStatement = brief.OralArgumentStatement,
                PublicationStatement = brief.PublicationStatement
            };

            return View(info);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}