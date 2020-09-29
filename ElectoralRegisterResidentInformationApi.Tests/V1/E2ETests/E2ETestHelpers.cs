using System;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class E2ETestHelpers : IntegrationTests<Startup>
    {
        private static readonly IFixture _fixture = new Fixture();

        public static ResidentResponse SaveResidentsElectorRecordsToTheDatabase()
        {
            var property = _fixture.Build<ElectorsProperty>()
                .With(e => e.Uprn, _fixture.Create<int>().ToString).Create();
            ElectoralRegisterContext.Properties.Add(property);
            ElectoralRegisterContext.SaveChanges();

            var elector = _fixture.Build<Elector>()
                .Without(e => e.ElectorsProperty)
                .Without(e => e.ElectorExtension)
                .With(e => e.PropertyId, property.Id)
                .Create();
            ElectoralRegisterContext.Electors.Add(elector);
            ElectoralRegisterContext.SaveChanges();

            var electorExtension = _fixture.Build<ElectorExtension>()
                .With(e => e.Id, elector.Id)
                .Create();
            ElectoralRegisterContext.ElectorExtensions.Add(electorExtension);
            ElectoralRegisterContext.SaveChanges();

            var expectedResponse = new ResidentResponse
            {
                Id = elector.Id,
                Email = elector.Email,
                Nationality = elector.Nationality,
                Title = elector.Title,
                Uprn = Convert.ToInt32(property.Uprn),
                FirstName = elector.FirstName,
                LastName = elector.LastName,
                MiddleName = electorExtension.MiddleName,
                DateOfBirth = electorExtension.DateOfBirth.ToString("yyyy-MM-dd")
            };
            return expectedResponse;
        }
    }
}
