using System.Collections.Generic;
using System.Linq;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Factories;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

        public List<Resident> GetAll()
        {
            return new List<Resident>();
        }
    }
}
