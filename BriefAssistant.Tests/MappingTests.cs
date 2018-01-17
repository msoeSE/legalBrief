using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BriefAssistant.Data;
using BriefAssistant.Models;
using Xunit;

namespace BriefAssistant.Tests
{
    public class MappingTests
    {
        private BriefInfo CreateBriefInfo()
        {
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
                    CaseNumber = "2018-EX-000001",
                    County = County.Milwaukee,
                    JudgeFirstName = "Bob",
                    JudgeLastName = "Smith",
                    OpponentName = "Dave Jones",
                    Role = Role.Plaintiff
                },
                Argument = "example arg",
                OralArgumentStatement = "example oral arg",
                CaseFactsStatement = "example case and facts",
                Conclusion = "example conclusion",
                AppendixDocuments = "example doc 1\nexample doc 2",
                PublicationStatement = "example publication"
            };

            return brief;
        }

        [Fact]
        public void MapperConfigIsVaild()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            
        }

        [Fact]
        public void MappedBriefDtoHasSameArgument()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.Argument, result.Argument);
        }

        [Fact]
        public void MappedBriefDtoHasSameAppellateCourtCaseNumber()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.AppellateCourtCaseNumber, result.AppellateCourtCaseNumber);
        }

        [Fact]
        public void MappedBriefDtoHasSameAppendixDocuments()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.AppendixDocuments, result.AppendixDocuments);
        }

        [Fact]
        public void MappedBriefDtoHasSameCaseFactsStatement()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CaseFactsStatement, result.CaseFactsStatement);
        }

        [Fact]
        public void MappedBriefDtoHasSameConclusion()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.Conclusion, result.Conclusion);
        }

        [Fact]
        public void MappedBriefDtoHasSameIssuesPresented()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.IssuesPresented, result.IssuesPresented);
        }

        [Fact]
        public void MappedBriefDtoHasSameOralArgumentStatement()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.OralArgumentStatement, result.OralArgumentStatement);
        }

        [Fact]
        public void MappedBriefDtoHasSamePublicationStatement()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.PublicationStatement, result.PublicationStatement);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseNumber()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.CaseNumber, result.CaseDto.CaseNumber);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseCounty()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.County, result.CaseDto.County);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseJudgeFirstName()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.JudgeFirstName, result.CaseDto.JudgeFirstName);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseJudgeLastName()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.JudgeLastName, result.CaseDto.JudgeLastName);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseOpponentName()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.OpponentName, result.CaseDto.OpponentName);
        }

        [Fact]
        public void MappedBriefDtoHasSameCircuitCourtCaseRole()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.CircuitCourtCase.Role, result.CaseDto.Role);
        }

        [Fact]
        public void MappedBriefDtoHasSameEmail()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Email, result.ContactInfoDto.Email);
        }

        [Fact]
        public void MappedBriefDtoHasSamePhone()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Phone, result.ContactInfoDto.Phone);
        }

        [Fact]
        public void MappedBriefDtoHasSameStreet()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Address.Street, result.ContactInfoDto.Street);
        }

        [Fact]
        public void MappedBriefDtoHasSameStreet2()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Address.Street2, result.ContactInfoDto.Street2);
        }

        [Fact]
        public void MappedBriefDtoHasSameCity()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Address.City, result.ContactInfoDto.City);
        }

        [Fact]
        public void MappedBriefDtoHasSameState()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Address.State, result.ContactInfoDto.State);
        }

        [Fact]
        public void MappedBriefDtoHasSameZip()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var source = CreateBriefInfo();

            var result = Mapper.Map<BriefDto>(source);

            Assert.Equal(source.ContactInfo.Address.Street2, result.ContactInfoDto.Street2);
        }
    }
}
