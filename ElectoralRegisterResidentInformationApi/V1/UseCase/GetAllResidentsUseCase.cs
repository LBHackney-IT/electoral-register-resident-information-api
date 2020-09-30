using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase
{
    public class GetAllResidentsUseCase : IGetAllResidentsUseCase
    {
        private readonly IElectoralRegisterGateway _electoralRegisterGateway;
        public GetAllResidentsUseCase(IElectoralRegisterGateway electoralRegisterGateway)
        {
            _electoralRegisterGateway = electoralRegisterGateway;
        }

        public ResidentInformationList Execute()
        {
            var residents = _electoralRegisterGateway.GetAllResidents();
            return new ResidentInformationList { Residents = residents.ToResponse() };
        }
    }
}
