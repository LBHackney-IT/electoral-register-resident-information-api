using System.Collections.Generic;
using ElectoralRegisterResidentInformationApi.V1.Domain;

namespace ElectoralRegisterResidentInformationApi.V1.Gateways
{
    public interface IElectoralRegisterGateway
    {
        Resident GetEntityById(int id);

        List<Resident> GetAllResidents();
    }
}
