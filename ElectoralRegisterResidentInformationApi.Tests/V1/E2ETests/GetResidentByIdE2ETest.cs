using System;
using System.Net.Http;
using System.Threading.Tasks;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class GetResidentByIdTest : IntegrationTests<Startup>
    {
        [Test]
        public async Task GetResidentByIdWillReturnResidentDetailsIfFound()
        {
            var expectedResident = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext);
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
    }
}
