using System.Collections.Generic;
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
                    FirstName = "test",
                    LastName = "test",
                    DateOfBirth = "01/01/2020",
                    Uprn = 1234,
                    Email = "test@email.com"
                }
            };

            var residentInformationList = new ResidentInformationList
            {
                Residents = residentInfo
            };

            _mockGetAllResidentsUseCase.Setup(x => x.Execute()).Returns(residentInformationList);
            var response = _classUnderTest.ListResidents() as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentInformationList);
        }
    }
}
