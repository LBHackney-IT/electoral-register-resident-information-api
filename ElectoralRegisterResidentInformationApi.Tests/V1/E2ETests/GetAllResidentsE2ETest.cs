using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.E2ETests
{
    public class GetAllResidentsE2ETest : IntegrationTests<Startup>
    {
        private readonly Faker _faker = new Faker();
        [Test]
        public async Task IfNoQueryParametersWillReturnAllResidentRecordsAndRelatedDataFromTheElectoralRegister()
        {
            var expectedResidentOne = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext);
            var expectedResidentTwo = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext);
            var expectedResidentThree = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext);

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

        [Test]
        public async Task IQueryParametersAreProvidedWillReturnAllResidentRecordsThatMatchQuery()
        {
            var firstName = _faker.Person.FirstName;
            var lastName = _faker.Person.LastName;
            var expectedResidentOne = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext, firstName, lastName);
            var expectedResidentTwo = E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext, firstName, lastName);
            E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext); //should not be returned

            var listUri = new Uri($"/api/v1/residents?first_name={firstName}&last_name={lastName}", UriKind.Relative);

            var response = Client.GetAsync(listUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentInformationList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(2);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentOne);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentTwo);
        }
        [Test]
        public async Task IfNumberOfResidentsInDbIsLessThanMaxLimitNextCursorShouldBeNull()
        {
            var firstName = _faker.Person.FirstName;
            var lastName = _faker.Person.LastName;
            var addMultipleResidents = Enumerable.Range(0, 10)
                .Select(x =>
                    E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext, firstName, lastName, x + 1))
                .ToList();
            var listUri = new Uri($"/api/v1/residents?first_name={firstName}&last_name={lastName}", UriKind.Relative);
            var response = Client.GetAsync(listUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentInformationList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(addMultipleResidents.Count);
            convertedResponse.NextCursor.Should().Be(null);
        }


        [Test]
        public async Task IfLimitAndCursorIsSuppliedShouldReturnCorrectSetOfTokens()
        {
            var firstName = _faker.Person.FirstName;
            var addMultipleResidents = Enumerable.Range(0, 15) //insert a number of tokens that given the limit and cursor would make 'NextCursor' null
                .Select(x =>
                       E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext, firstName, null, x + 1))
                .ToList();

            var listUri = new Uri($"/api/v1/residents?first_name={firstName}&limit=11&cursor=11", UriKind.Relative);
            var response = Client.GetAsync(listUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentInformationList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(4);
            convertedResponse.NextCursor.Should().Be(null);
        }
        [Test]
        public async Task IfManyTokensInDbAndLimitAndCursorIsSuppliedShouldReturnCorrectSetOfTokens()
        {
            var firstName = _faker.Person.FirstName;
            var addMultipleResidents = Enumerable.Range(0, 35) //insert multiple tokens that will require a 'NextCursor' to be returned
                .Select(x =>
                       E2ETestHelpers.SaveResidentsElectorRecordsToTheDatabase(ElectoralRegisterContext, firstName, null, x + 1))
                .ToList();

            var listUri = new Uri($"/api/v1/residents?first_name={firstName}&limit=11&cursor=11", UriKind.Relative);
            var response = Client.GetAsync(listUri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentInformationList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(11);
            convertedResponse.NextCursor.Should().Be("22");
            convertedResponse.Residents.Should().BeEquivalentTo(addMultipleResidents.Skip(11).Take(11));
        }
    }
}
