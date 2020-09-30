using System.Linq;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<IElectoralRegisterGateway> _mockGateway;
        private GetAllResidentsUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IElectoralRegisterGateway>();
            _classUnderTest = new GetAllResidentsUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public void ReturnsResidentInformationList()
        {
            var stubbedResidents = _fixture.CreateMany<Resident>().ToList();
            _mockGateway.Setup(x => x.GetAllResidents()).Returns(stubbedResidents);

            var response = _classUnderTest.Execute();

            response.Should().NotBeNull();
            response.Residents.Should().BeEquivalentTo(stubbedResidents.ToResponse());
        }
    }
}
