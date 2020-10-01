using System;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class E2ETestHelpers : IntegrationTests<Startup>
    {
        private static readonly IFixture _fixture = new Fixture();

        public static ResidentResponse SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext context, string firstName = null, string lastName = null, int id = 0)
        {
            var property = _fixture.Build<ElectorsProperty>()
                .With(e => e.Uprn, _fixture.Create<int>().ToString).Create();
            context.Properties.Add(property);
            context.SaveChanges();

            var elector = _fixture.Build<Elector>()
                .Without(e => e.ElectorsProperty)
                .Without(e => e.ElectorExtension)
                .With(e => e.PropertyId, property.Id)
                .Create();
            elector.FirstName = firstName ?? elector.FirstName;
            elector.LastName = lastName ?? elector.LastName;
            if (id != 0) { elector.Id = id; }

            context.Electors.Add(elector);
            context.SaveChanges();

            var electorExtension = _fixture.Build<ElectorExtension>()
                .With(e => e.Id, elector.Id)
                .Create();
            context.ElectorExtensions.Add(electorExtension);
            context.SaveChanges();

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
