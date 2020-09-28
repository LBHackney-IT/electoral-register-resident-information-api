using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static Elector CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<Resident>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static Elector CreateDatabaseEntityFrom(Resident resident)
        {
            return new Elector
            {
                Id = resident.Id,
            };
        }
    }
}
