using System.Linq;
using ElectoralRegisterResidentInformationApi.Tests.V1.Helper;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class ElectoralRegisterContextTests : DatabaseTests
    {
        [Test]
        public void CanGetADatabaseEntity()
        {
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntity();

            ElectoralRegisterContext.Electors.Add(databaseEntity);
            ElectoralRegisterContext.SaveChanges();

            var result = ElectoralRegisterContext.Electors.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(databaseEntity);
        }
    }
}
