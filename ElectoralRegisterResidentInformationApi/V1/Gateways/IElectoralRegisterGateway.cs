using System.Collections.Generic;
using ElectoralRegisterResidentInformationApi.V1.Domain;

namespace ElectoralRegisterResidentInformationApi.V1.Gateways
{
    public interface IElectoralRegisterGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
