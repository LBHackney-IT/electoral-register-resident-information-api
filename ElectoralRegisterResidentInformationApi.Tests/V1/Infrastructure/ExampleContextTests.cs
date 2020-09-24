using System.Linq;
using ElectoralRegisterResidentInformationApi.Tests.V1.Helper;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class DatabaseContextTest : DatabaseTests
    {
        [Test]
        public void CanGetADatabaseEntity()
        {
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntity();

            DatabaseContext.Add(databaseEntity);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.DatabaseEntities.ToList().FirstOrDefault();

            Assert.AreEqual(result, databaseEntity);
        }
    }
}