using ElectoralRegisterResidentInformationApi.V1.Boundary.Request;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ElectoralRegisterResidentInformationApi.V1.UseCase
{
    public class GetAllResidentsUseCase : IGetAllResidentsUseCase
    {
        private readonly IElectoralRegisterGateway _electoralRegisterGateway;
        public GetAllResidentsUseCase(IElectoralRegisterGateway electoralRegisterGateway)
        {
            _electoralRegisterGateway = electoralRegisterGateway;
        }

        public ResidentInformationList Execute(ListResidentsRequest request)
        {
            var limit = request.Limit < 10 ? 10 : request.Limit;
            limit = request.Limit > 100 ? 100 : limit;
            var residents = _electoralRegisterGateway.GetAllResidents(request).ToResponse();

            return new ResidentInformationList
            {
                Residents = residents,
                NextCursor = GetNextCursor(residents, limit)
            };
        }
        private static string GetNextCursor(List<ResidentResponse> residents, int limit)
        {
            return residents.Count == limit ? residents.Max(r => r.Id).ToString() : null;
        }
    }
}
