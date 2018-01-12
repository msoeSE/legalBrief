using BriefAssistant.Controllers;
using BriefAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.AspNetCore.Mvc;



namespace BriefAssistant.Tests
{
    
    class DataControllerTests
    {

        public void Save_briefInfo_to_DB() {
            var options = new DbContextOptionsBuilder<Brief_assistantContext>()
                .UseInMemoryDatabase(databaseName: "Add_briefInfo_to_DB")
                .Options;
            using (var context = new Brief_assistantContext(options))
            {
                var controller = new DataController(context);
                controller.SaveRecordforUser(new BriefInfo
                {
                    Appellant = new Appellant()
                    {
                        Address = new Address
                        {
                            Street = "1234 Main St.",
                            Street2 = "Apt 568",
                            City = "Milwaukee",
                            State = State.WI,
                            Zip = "53202"
                        },

                        Email = "test@example.com",
                        Name = "John Doe",
                        Phone = "123-456-7890"
                    },

                    CircuitCourtCase = new CircuitCourtCase
                    {
                        CaseNumber = "2017AP0001",
                        County = County.Milwaukee,
                        JudgeFirstName = "Bob",
                        JudgeLastName = "Smith",
                        OpponentName = "Jane Doe",
                        Role = Role.Plaintiff
                    },
                    IssuesPresented = "Testing IssuesPresented",
                    Conclusion = "Testing Conclusion",
                    AppendexDocuments ="Testing AppendexDocuments",
                    Argument = "Testing Arguement",
                    CaseFactsStatement = "Testing CaseFactsStatement",
                    OralArgumentStatement = "Testing OralArgumentStatement",
                    PublicationStatement = "Testing PublicationStatement"


                });
            }
            using (var context = new Brief_assistantContext(options))
            {
                Assert.AreEqual("John Doe", context.UserInfo.Single().Name);
            }
        }
        public void Retrieve_ExistingUserInfo_from_DB()
        {
            var options = new DbContextOptionsBuilder<Brief_assistantContext>()
               .UseInMemoryDatabase(databaseName: "Add_briefInfo_to_DB")
               .Options;
            using (var context = new Brief_assistantContext(options))
            {
                var controller = new DataController(context);
                controller.SaveRecordforUser(new BriefInfo
                {
                    Appellant = new Appellant()
                    {
                        Address = new Address
                        {
                            Street = "1234 Main St.",
                            Street2 = "Apt 568",
                            City = "Milwaukee",
                            State = State.WI,
                            Zip = "53202"
                        },

                        Email = "user1@example.com",
                        Name = "John Doe",
                        Phone = "123-456-7890"
                    },

                    CircuitCourtCase = new CircuitCourtCase
                    {
                        CaseNumber = "2017AP0001",
                        County = County.Milwaukee,
                        JudgeFirstName = "Bob",
                        JudgeLastName = "Smith",
                        OpponentName = "Jane Doe",
                        Role = Role.Plaintiff
                    },
                    IssuesPresented = "Testing IssuesPresented",
                    Conclusion = "Testing Conclusion",
                    AppendexDocuments = "Testing AppendexDocuments",
                    Argument = "Testing Arguement",
                    CaseFactsStatement = "Testing CaseFactsStatement",
                    OralArgumentStatement = "Testing OralArgumentStatement",
                    PublicationStatement = "Testing PublicationStatement"


                });
            }          
            using (var context = new Brief_assistantContext(options))
            {
                var controller = new DataController(context);
                controller.SaveRecordforUser(new BriefInfo
                {
                    Appellant = new Appellant()
                    {
                        Address = new Address
                        {
                            Street = "1222 Main St.",
                            Street2 = "Apt 123",
                            City = "Milwaukee",
                            State = State.WI,
                            Zip = "53202"
                        },

                        Email = "user2@example.com",
                        Name = "Jane Doe",
                        Phone = "123-456-7890"
                    },

                    CircuitCourtCase = new CircuitCourtCase
                    {
                        CaseNumber = "2017AP0002",
                        County = County.Milwaukee,
                        JudgeFirstName = "Bob",
                        JudgeLastName = "Smith",
                        OpponentName = "Jane Doe",
                        Role = Role.Plaintiff
                    },
                    IssuesPresented = "Testing IssuesPresented",
                    Conclusion = "Testing Conclusion",
                    AppendexDocuments = "Testing AppendexDocuments",
                    Argument = "Testing Arguement",
                    CaseFactsStatement = "Testing CaseFactsStatement",
                    OralArgumentStatement = "Testing OralArgumentStatement",
                    PublicationStatement = "Testing PublicationStatement"


                });

            }
            using (var context = new Brief_assistantContext(options))
            {
                var controller = new DataController(context);
                IActionResult result = controller.RetrieveInfo(new EmailRequest { Email = "user2@example.com" });
                JsonResult jsonResult = result as JsonResult;
                Assert.IsInstanceOfType(result, typeof(JsonResult));
            }

        }
        public void Retrieve_NotExistingUSerInfo_from_DB()
        {
            var options = new DbContextOptionsBuilder<Brief_assistantContext>()
                .UseInMemoryDatabase(databaseName: "Retrieve_NotExistingUSerInfo_from_DB")
                .Options;
            using (var context = new Brief_assistantContext(options))
            {
                var controller = new DataController(context);
                IActionResult result = controller.RetrieveInfo(new EmailRequest { Email = "newUser@google.com" });
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

    }

}



