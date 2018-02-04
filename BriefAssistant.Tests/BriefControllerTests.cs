using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BriefAssistant.Data;
using BriefAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
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

        //Happy Path for CreateAsync()
        [Fact]
        public async Task Create_ReturnsCreatedResult_WhenRequestIsValid()
        {
            // Arrange
            var b = new BriefInfo
            {
                AppellateCase = new AppellateCase(),
                AppellateCourtCaseNumber = "2017AP1",
                AppendixDocuments = "example 1\nexample2",
                Argument = "argument",
                CaseFactsStatement = "CF",
                CircuitCourtCase = new CircuitCourtCase
                {
                    CaseNumber = "2018-AA-000001",
                    County = County.Milwaukee,
                    JudgeFirstName = "Bob",
                    JudgeLastName = "Smith",
                    OpponentName = "Dave Jones",
                    Role = Role.Plaintiff
                },
                Conclusion = "conclusion",
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
                Date = "date",
                Id = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 11),
                OralArgumentStatement = "oral",
                PublicationStatement = "publication",
                IssuesPresented = "issues",
                Title = "title"
            };
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

            var httpContent = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PostAsync("/api/briefs/", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var brief = new BriefInfo
            {
                //Missing ContactInfo
                
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(brief), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PostAsync("/api/briefs/", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //Happy Path for GetBriefsAsync()
        [Fact]
        public async Task GetBriefsAsync_ReturnsJson_WhenUserIsNotNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "getBriefsAsyncTestDB").Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Briefs.Add(new BriefDto
                {
                    AppellateCourtCaseNumber = "2017AP1",
                    AppendixDocuments = "a\nb\nc",
                    ApplicationUserId = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
                    Argument = "none",
                    CaseFactsStatement = "none",
                    CircuitCourtCaseId = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
                    CircuitCourtCaseDto = new CircuitCourtCaseDto(),
                    Conclusion = "none",
                    ContactInfoDto = new ContactInfoDto(),
                    ContactInfoId = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
                    Id = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
                    IssuesPresented = "none",
                    Name = "Brief1",
                    OralArgumentStatement = "none",
                    PublicationStatement = "none"
                });
                context.Cases.Add(new CircuitCourtCaseDto
                {
                    ApplicationUserId = new Guid(),
                    BriefDto = new List<BriefDto>(),
                    CaseNumber = "2017CV1",
                    County = County.Adams,
                    Id = new Guid(),
                    JudgeFirstName = "Judge",
                    JudgeLastName = "Jo",
                    OpponentName = "opp",
                    Role = Role.Defendent
                });
                context.Contacts.Add(new ContactInfoDto
                {
                    ApplicationUserId = new Guid(),
                    BriefDto = new List<BriefDto>(),
                    City = "Milwaukee",
                    Email = "test@test.com",
                    Id = 1,
                    Name = "Bob Tom",
                    Phone = "555-555-5555",
                    State = State.WI,
                    Street = "123 Milwaukee Street",
                    Street2 = "",
                    Zip = "53202"
                });
            }

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //Happy Path for GetAsync()
        [Fact]
        public async Task GetAsync_ReturnsJson_WhenIdExistsInDB()
        {
            // Arrange
            var mockset = new Mock<ApplicationDbContext>();
            var info = new BriefInfo
            {

            };
            var user = new Mock<ApplicationUser>();

            var infoDto = Mapper.Map<BriefDto>(info);
            mockset.Object.Briefs.Add(infoDto);

            var id = "abcdefg";

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_ReturnsBadRequest_WhenIdIsNull()
        {
            // Arrange
            object id = null;

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenIdIsNotInDB()
        {
            // Arrange
            object id = "notFound";

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_ReturnsForbidden_WhenUserNotAuthorized()
        {
            // Arrange
            object id = "notAuthorized";

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task DownloadBrief_ReturnsNotFound_WhenBriefDNE()
        {
            // Arrange
            var id = "fileNotFound";

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/download/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task EmailBrief_ReturnsNotFound_WhenBriefDNE()
        {
            // Arrange
            var id = "fileNotFound";

            // Act
            var response = await _testFixture.Client.GetAsync("/api/briefs/email/" + id);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task EmailBrief_ReturnsBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var id = "file";
            var email = new EmailRequest { }; //Missing email

            var httpContent = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PostAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //Happy Path for EmailBrief()
        [Fact]
        public async Task EmailBrief_ReturnsNoContent_WhenEmailSuccessful()
        {
            // Arrange
            var id = "goodId";
            var email = new EmailRequest
            {
                Email = "goodEmail@email.com"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PostAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        //Happy Path for UpdateAsync()
        [Fact]
        public async Task UpdateAsync_ReturnsNothing_WhenUpdateSuccessful()
        {
            // Arrange
            var id = "goodId";
            var info = new BriefInfo
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

            var httpContent = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PutAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var id = "goodId";
            var info = new BriefInfo
            {
                ContactInfo = new ContactInfo
                {
                    //Missing Name
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

            var httpContent = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PutAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNotFound_WhenBriefNotFound()
        {
            // Arrange
            var id = "goodId";
            var info = new BriefInfo
            {
                ContactInfo = new ContactInfo
                {
                    Name = "Not The Right Name",
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

            var httpContent = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PutAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsForbidden_WhenAuthenticationFailed()
        {
            // Arrange
            var id = "goodId";
            var info = new BriefInfo
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

            var httpContent = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

            // Act
            var response = await _testFixture.Client.PutAsync("/api/briefs/email/" + id, httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}