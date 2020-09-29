using System;
using System.Threading.Tasks;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class GetAllResidentsE2ETest : IntegrationTests<Startup>
    {
        [Test]
        public async Task IfNoQueryParametersWillReturnAllResidentRecordsAndRelatedDataFromTheElectoralRegister()
        {
            var expectedResidentOne = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase();
            var expectedResidentTwo = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase();
            var expectedResidentThree = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase();

            var listUri = new Uri("/api/v1/residents", UriKind.Relative);

            var response = Client.GetAsync(listUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentInformationList>(stringContent);

            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentOne);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentTwo);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentThree);
        }
    }
}
