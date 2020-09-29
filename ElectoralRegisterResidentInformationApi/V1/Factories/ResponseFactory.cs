using System.Collections.Generic;
using System.Linq;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Domain;

namespace ElectoralRegisterResidentInformationApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static ResidentResponse ToResponse(this Resident resident)
        {
            return new ResidentResponse
            {
                Id = resident.Id,
                Title = resident.Title,
                FirstName = resident.FirstName,
                MiddleName = resident.MiddleName,
                LastName = resident.LastName,
                Nationality = resident.Nationality,
                DateOfBirth = resident.DateOfBirth?.ToString("yyyy-MM-dd"),
                Email = resident.Email,
                Uprn = resident.Uprn
            };
        }

        public static List<ResidentResponse> ToResponse(this IEnumerable<Resident> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
