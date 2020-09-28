using System;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using FluentAssertions;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var domain = new Resident
            {
                Id = 4,
                Email = "email@the-server.uk",
                Nationality = "French",
                Title = "Dct.",
                Uprn = 324325315,
                FirstName = "MyFirstName",
                MiddleName = "James",
                LastName = "The end name",
                DateOfBirth = new DateTime(1977, 07, 03)
            };
            var response = domain.ToResponse();

            response.Id.Should().Be(4);
            response.Email.Should().Be("email@the-server.uk");
            response.Nationality.Should().Be("French");
            response.Title.Should().Be("Dct.");
            response.Uprn.Should().Be(324325315);
            response.FirstName.Should().Be("MyFirstName");
            response.LastName.Should().Be("The end name");
            response.MiddleName.Should().Be("James");
            response.DateOfBirth.Should().Be("1977-07-03");
        }
    }
}
