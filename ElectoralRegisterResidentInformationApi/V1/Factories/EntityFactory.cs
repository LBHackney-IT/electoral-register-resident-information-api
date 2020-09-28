using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Resident ToDomain(this DatabaseEntity databaseEntity)
        {
            //TODO: Map the rest of the fields in the domain object.
            // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings

            return new Resident
            {
                Id = databaseEntity.Id,
            };
        }
    }
}
