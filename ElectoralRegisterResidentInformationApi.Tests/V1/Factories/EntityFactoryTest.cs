using System;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        [Test]
        public void CanMapAnElectorEntityToAResidentDomainObject()
        {
            var elector = new Elector
            {
                Id = 77,
                Email = "contact-me@my-email.co.uk",
                Nationality = "Russian",
                Title = "Prof.",
                FirstName = "Green",
                LastName = "White",
            };
            var resident = elector.ToDomain();

            resident.Id.Should().Be(77);
            resident.Email.Should().Be("contact-me@my-email.co.uk");
            resident.Nationality.Should().Be("Russian");
            resident.Title.Should().Be("Prof.");
            resident.FirstName.Should().Be("Green");
            resident.LastName.Should().Be("White");
        }

        [Test]
        public void CanMapAnElectorWithAttachedPropertyToResidentDomainObject()
        {
            var elector = new Elector
            {
                Id = 77,
                ElectorsProperty = new ElectorsProperty
                {
                    Id = 88,
                    Uprn = "9842274",
                }
            };
            var resident = elector.ToDomain();

            resident.Id.Should().Be(77);
            resident.Uprn.Should().Be(9842274);
        }

        [Test]
        public void CanMapAnElectorWithAttachedElectorExtensionToResidentDomain()
        {
            var electorExtension = new Elector
            {
                Id = 83,
                ElectorExtension = new ElectorExtension
                {
                    Id = 77,
                    MiddleName = "John",
                    DateOfBirth = new DateTime(2001, 04, 05)
                }
            };
            var resident = electorExtension.ToDomain();

            resident.Id.Should().Be(83);
            resident.MiddleName.Should().Be("John");
            resident.DateOfBirth.Should().Be(new DateTime(2001, 04, 05));
        }
    }
}
