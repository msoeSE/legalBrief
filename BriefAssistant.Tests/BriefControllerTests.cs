using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BriefAssistant.Models;
using Newtonsoft.Json;
using Xunit;

namespace BriefAssistant.Tests
{
    public class BriefControllerTests
    {
        private readonly TestFixture<Startup> _testFixture;

        public BriefControllerTests()
        {
            _testFixture = new TestFixture<Startup>();
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WhenRequestIsValid()
        {
            // Arrange
            var brief = new BriefInfo
            {
                ContactInfo = new ContactInfo
                {
                    Name = "John Doe",
                    Address = new Address
                    {
                        Street = "1234 Main St.",
                        City = "Milwaukee",
                        State = State.WI,
                        Zip = "53202"
                    },
                    Email = "johndoe@example.com",
                    Phone = "608-216-8689"
                },
                CircuitCourtCase = new CircuitCourtCase
                {
                    CaseNumber = "2018-AA-000001",
                    County = County.Milwaukee,
                    JudgeFirstName = "Bob",
                    JudgeLastName = "Smith",
                    OpponentName = "Dave Jones",
                    Role = Role.Plaintiff
                },
                Argument = "example",
                OralArgumentStatement = "example",
                CaseFactsStatement = "example",
                Conclusion = "example",
                AppendixDocuments = "example 1\nexample2",
                PublicationStatement = "example"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(brief), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.("/api/briefs/", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}