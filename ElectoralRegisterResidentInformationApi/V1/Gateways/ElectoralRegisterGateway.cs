using System.Collections.Generic;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class ElectoralRegisterGateway : IElectoralRegisterGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ElectoralRegisterGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Resident GetEntityById(int id)
        {
            var result = _databaseContext.DatabaseEntities.Find(id);

            return result?.ToDomain();
        }

        public List<Resident> GetAll()
        {
            return new List<Resident>();
        }
    }
}
