using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces
{
    public interface IGetResidentByIdUseCase
    {
        ResidentResponse Execute(int id);
    }
}
