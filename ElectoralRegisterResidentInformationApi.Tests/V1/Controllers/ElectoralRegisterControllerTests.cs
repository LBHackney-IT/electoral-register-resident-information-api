using System.Collections.Generic;
using Bogus;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Request;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Controllers;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ElectoralRegisterControllerTests
    {
        private ElectoralRegisterController _classUnderTest;
        private Mock<IGetAllResidentsUseCase> _mockGetAllResidentsUseCase;
        private Mock<IGetResidentByIdUseCase> _mockGetResidentByIdUseCase;
        private readonly Faker _faker = new Faker();

        [SetUp]
        public void SetUp()
        {
            _mockGetAllResidentsUseCase = new Mock<IGetAllResidentsUseCase>();
            _mockGetResidentByIdUseCase = new Mock<IGetResidentByIdUseCase>();
            _classUnderTest = new ElectoralRegisterController(_mockGetAllResidentsUseCase.Object, _mockGetResidentByIdUseCase.Object);
        }

        [Test]
        public void ListResidents()
        {

            var residentInfo = new List<ResidentResponse>()
            {
                new ResidentResponse
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    DateOfBirth = _faker.Date.Past().ToString(),
                    Uprn = 1234,
                    Email = _faker.Person.Email
                }
            };

            var residentInformationList = new ResidentInformationList
            {
                Residents = residentInfo
            };

            var request = new ListResidentsRequest()
            {
                FirstName = residentInfo[0].FirstName,
                LastName = residentInfo[0].LastName
            };

            _mockGetAllResidentsUseCase.Setup(x => x.Execute(request)).Returns(residentInformationList);
            var response = _classUnderTest.ListResidents(request) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentInformationList);
        }
    }
}
