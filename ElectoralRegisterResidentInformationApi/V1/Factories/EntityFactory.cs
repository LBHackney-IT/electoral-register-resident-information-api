using System;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;

namespace ElectoralRegisterResidentInformationApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Resident ToDomain(this Elector elector)
        {
            return new Resident
            {
                Id = elector.Id,
                Email = elector.Email,
                Nationality = elector.Nationality,
                Title = elector.Title,
                Uprn = elector.ElectorsProperty?.Uprn != null ? Convert.ToInt32(elector.ElectorsProperty?.Uprn) : (int?) null,
                FirstName = elector.FirstName,
                LastName = elector.LastName,
            };
        }

        public static Resident ToDomain(this ElectorExtension electorExtension)
        {
            var resident = electorExtension.Elector != null ? electorExtension.Elector.ToDomain() : new Resident();
            resident.DateOfBirth = electorExtension.DateOfBirth;
            resident.MiddleName = electorExtension.MiddleName;
            return resident;
        }
    }
}
