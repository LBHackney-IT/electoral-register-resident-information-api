using System;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IElectoralRegisterGateway> _mockGateway;
        private GetResidentByIdUseCase _classUnderTest;
        private readonly IFixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IElectoralRegisterGateway>();
            _classUnderTest = new GetResidentByIdUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteReturnsResidentDetailsFromTheGateway()
        {
            var residentId = _fixture.Create<int>();
            var gatewayResponse = _fixture.Create<Resident>();
            _mockGateway.Setup(g => g.GetEntityById(residentId))
                .Returns(gatewayResponse);
            _classUnderTest.Execute(residentId).Should().BeEquivalentTo(gatewayResponse.ToResponse());
        }

        [Test]
        public void IfTheGatewayReturnsNullExecuteThrowsNotFoundException()
        {
            _mockGateway.Setup(g => g.GetEntityById(It.IsAny<int>()))
                .Returns((Resident) null);
            Func<ResidentResponse> testDelegate = () => _classUnderTest.Execute(5);
            testDelegate.Should().Throw<ResidentNotFoundException>();
        }
    }
}
