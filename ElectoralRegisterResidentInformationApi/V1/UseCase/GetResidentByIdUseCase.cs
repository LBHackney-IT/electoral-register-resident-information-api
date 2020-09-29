using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase
{
    public class GetResidentByIdUseCase : IGetResidentByIdUseCase
    {
        private readonly IElectoralRegisterGateway _gateway;
        public GetResidentByIdUseCase(IElectoralRegisterGateway gateway)
        {
            _gateway = gateway;
        }

        public ResidentResponse Execute(int id)
        {
            var resident = _gateway.GetEntityById(id);
            if (resident == null) throw new ResidentNotFoundException();

            return resident.ToResponse();
        }
    }
}
