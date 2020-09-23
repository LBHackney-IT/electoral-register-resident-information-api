using System.Collections.Generic;
using ElectoralRegisterResidentInformationApi.V1.Domain;

namespace ElectoralRegisterResidentInformationApi.V1.Gateways
{
    public interface IElectoralRegisterResidentInformationApiGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
