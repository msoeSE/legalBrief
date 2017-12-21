using BriefAssistant.Controllers;
using BriefAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace BriefAssistant.Tests
{
    
    class DataControllerTests
    {
        
        public void Add_briefInfo_to_DB() {
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

    }

}



