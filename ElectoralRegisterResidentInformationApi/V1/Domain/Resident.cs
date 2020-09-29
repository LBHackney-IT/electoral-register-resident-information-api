using System;

namespace ElectoralRegisterResidentInformationApi.V1.Domain
{
    public class Resident
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public int? Uprn { get; set; }
    }
}
