using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class GetResidentByIdTest : IntegrationTests<Startup>
    {
        private readonly IFixture _fixture = new Fixture();

        [Test]
        public async Task GetResidentByIdWillReturnResidentDetailsIfFound()
        {
            var expectedResident = SaveResidentsElectorRecordsToTheDatabase();
            var response = await CallEndpointWithId(expectedResident.Id).ConfigureAwait(true);
            response.StatusCode.Should().Be(200);

            var data = await ConvertToResponseObject(response).ConfigureAwait(true);
            data.Should().BeEquivalentTo(expectedResident);
        }

        [Test]
        public async Task GetResidentDetailsWillReturn404IfNotFound()
        {
            var response = await CallEndpointWithId(7).ConfigureAwait(true);
            response.StatusCode.Should().Be(404);
        }

        private async Task<HttpResponseMessage> CallEndpointWithId(int id)
        {
            var url = new Uri($"/api/v1/residents/{id}", UriKind.Relative);
            return await Client.GetAsync(url).ConfigureAwait(true);
        }

        private static async Task<ResidentResponse> ConvertToResponseObject(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return JsonConvert.DeserializeObject<ResidentResponse>(data);
        }

        private ResidentResponse SaveResidentsElectorRecordsToTheDatabase()
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
