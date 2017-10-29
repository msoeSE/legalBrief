using System;
using System.Collections.Generic;
using System.Text;
using BriefAssistant.Controllers;
using BriefAssistant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BriefAssistant.Tests
{
    public class BriefControllerTests
    {
        [Fact]
        public void Create_ReturnsCreatedResult_WhenRequestIsValid()
        {
            var controller = new BriefController();
            var request = new BriefInfo()
            {
                Appellant = new Appellant()
                {
                    Address = new Address
                    {
                        Street = "1234 Main St.",
                        Streee2 = "Apt 568",
                        City = "Milwaukee",
                        State = State.WI,
                        Zip = "53202"
                    },

                    Email = "test@example.com",
                    Name = "John Doe",
                    Phone = "123-456-7890"
                },

                CircutCourtCase = new CircutCourtCase
                {
                    CaseNumber = "2017AP0001",
                    County = County.Milwaukee,
                    JudgeFirstName = "Bob",
                    JudgeLastName = "Smith",
                    OpponentName = "Jane Doe",
                    Role = Role.Plantiff
                }
            };

            var result = controller.Post(request);
            Assert.IsType<CreatedResult>(result);
        }
    }
}
