using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces
{
    public interface IGetAllResidentsUseCase
    {
        ResidentInformationList Execute();
    }
}
