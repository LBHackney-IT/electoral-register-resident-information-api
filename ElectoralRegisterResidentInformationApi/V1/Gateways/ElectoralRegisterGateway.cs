using ElectoralRegisterResidentInformationApi.V1.Boundary.Request;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ElectoralRegisterResidentInformationApi.V1.Gateways
{
    public class ElectoralRegisterGateway : IElectoralRegisterGateway
    {
        private readonly ElectoralRegisterContext _electoralRegisterContext;

        public ElectoralRegisterGateway(ElectoralRegisterContext electoralRegisterContext)
        {
            _electoralRegisterContext = electoralRegisterContext;
        }

        public Resident GetEntityById(int id)
        {
            return _electoralRegisterContext.Electors
                .Include(e => e.ElectorExtension)
                .Include(e => e.ElectorsProperty)
                .FirstOrDefault(e => e.Id == id)?.ToDomain();
        }

        public List<Resident> GetAllResidents(ListResidentsRequest request)
        {
            return _electoralRegisterContext.Electors
                .Where(e => string.IsNullOrEmpty(request.FirstName) || e.FirstName.Trim().ToLower().Contains(request.FirstName.ToLower()))
                .Where(e => string.IsNullOrEmpty(request.LastName) || e.LastName.Trim().ToLower().Contains(request.LastName.ToLower()))
                .Where(e => e.Id > request.Cursor)
                .Include(e => e.ElectorExtension)
                .Include(e => e.ElectorsProperty)
                .OrderBy(e => e.Id)
                .Take(request.Limit)
                .ToList().ToDomain();
        }
    }
}
