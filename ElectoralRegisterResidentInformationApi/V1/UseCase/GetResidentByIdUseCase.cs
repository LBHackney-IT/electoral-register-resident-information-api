using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase
{
    public class GetResidentByIdUseCase : IGetResidentByIdUseCase
    {
        private IElectoralRegisterGateway _gateway;
        public GetResidentByIdUseCase(IElectoralRegisterGateway gateway)
        {
            _gateway = gateway;
        }

        public ResidentResponse Execute(int id)
        {
            return _gateway.GetEntityById(id).ToResponse();
        }
    }
}
