using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BriefAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace BriefAssistant.Controllers
{

    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly Brief_assistantContext _context;

        public DataController(Brief_assistantContext context)
        {
            _context=context;
        }

        [HttpPost("save")]
        public IActionResult SaveRecordforUser([FromBody]BriefInfo briefInfo)
        {

            if (_context.UserInfo.Any(o => o.Email == briefInfo.Appellant.Email))
            {
                var userInfo = _context.UserInfo
                    .Where(b => b.Email == briefInfo.Appellant.Email)
                    .FirstOrDefault();

                userInfo.Email = briefInfo.Appellant.Email;
                userInfo.Name = briefInfo.Appellant.Name;

                userInfo.Phone = briefInfo.Appellant.Phone;
                userInfo.State = briefInfo.Appellant.Address.State;
                userInfo.Street = briefInfo.Appellant.Address.Street;
                userInfo.Street2 = briefInfo.Appellant.Address.Street2;
                userInfo.Zip = briefInfo.Appellant.Address.Zip;
                userInfo.City = briefInfo.Appellant.Address.City;
                              
                _context.SaveChanges();

                var caseInfo = _context.CaseInfo
                                  .Where(b => b.UserId == userInfo.UserId)
                                  .FirstOrDefault();
                caseInfo.CaseNumber = briefInfo.CircuitCourtCase.CaseNumber;
                caseInfo.County = briefInfo.CircuitCourtCase.County;
                caseInfo.JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName;
                caseInfo.JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName;
                caseInfo.OpponentName = briefInfo.CircuitCourtCase.OpponentName;
                caseInfo.Role = briefInfo.CircuitCourtCase.Role;
                _context.SaveChanges();

                var brief = _context.BriefInfo
                      .Where(b => b.UserId == userInfo.UserId)
                      .FirstOrDefault();
                brief.AppendexDocuments = briefInfo.AppendexDocuments;
                brief.Argument = briefInfo.Argument;
                brief.CaseFactsStatement = briefInfo.CaseFactsStatement;
                brief.Conclusion = briefInfo.Conclusion;
                brief.IssuesPresented = briefInfo.IssuesPresented;
                brief.OralArgumentStatement = briefInfo.OralArgumentStatement;
                brief.PublicationStatement = briefInfo.PublicationStatement;
                _context.SaveChanges();
            }
            else {
                DbUserInfo user = new DbUserInfo
                {
                    Email = briefInfo.Appellant.Email,
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
                var userId = user.UserId;


                DbCaseInfo circuitCourtCase = new DbCaseInfo
                {
                    CaseNumber = briefInfo.CircuitCourtCase.CaseNumber,
                    County = briefInfo.CircuitCourtCase.County,
                    JudgeFirstName = briefInfo.CircuitCourtCase.JudgeFirstName,
                    JudgeLastName = briefInfo.CircuitCourtCase.JudgeLastName,
                    OpponentName = briefInfo.CircuitCourtCase.OpponentName,
                    Role = briefInfo.CircuitCourtCase.Role,
                    UserId = userId

                };
                _context.CaseInfo.Add(circuitCourtCase);
                _context.SaveChanges();
                var caseId = circuitCourtCase.CaseId;


               DbbriefInfo brief = new DbbriefInfo
                {

                    AppendexDocuments = briefInfo.AppendexDocuments,
                    Argument = briefInfo.Argument,
                    CaseFactsStatement = briefInfo.CaseFactsStatement,
                    Conclusion = briefInfo.Conclusion,
                    IssuesPresented = briefInfo.IssuesPresented,
                    OralArgumentStatement = briefInfo.OralArgumentStatement,
                    PublicationStatement = briefInfo.PublicationStatement,
                    UserId = userId,
                    CaseId = caseId

                };
                _context.BriefInfo.Add(brief);
                _context.SaveChanges();
            }
            return Ok();

        }

       
        [HttpPost("retrieve")]
        public IActionResult RetrieveInfo([FromBody]String email)
        {
            if (_context.UserInfo.Any(o => o.Email == email))
            {

                var user = _context.UserInfo
                                  .Where(b => b.Email == email)
                                  .FirstOrDefault();

                var caseInfo = _context.CaseInfo
                                  .Where(b => b.UserId == user.UserId)
                                  .FirstOrDefault();



                var brief = _context.BriefInfo
                      .Where(b => b.UserId == user.UserId)
                      .FirstOrDefault();
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
                    CaseNumber = caseInfo.CaseNumber,
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
                    AppendexDocuments = brief.AppendexDocuments,
                    Argument = brief.Argument,
                    CaseFactsStatement = brief.CaseFactsStatement,
                    Conclusion = brief.Conclusion,
                    IssuesPresented = brief.IssuesPresented,
                    OralArgumentStatement = brief.OralArgumentStatement,
                    PublicationStatement = brief.PublicationStatement
                };

                MemoryStream ms = new MemoryStream();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BriefInfo));
                ser.WriteObject(ms, info);
                byte[] json = ms.ToArray();
                ms.Close();
                return Ok(Encoding.UTF8.GetString(json, 0, json.Length));
            }
            else {
                return NotFound();
            }


        }
        public IActionResult Index()
        {
            return View();
        }
    }
}