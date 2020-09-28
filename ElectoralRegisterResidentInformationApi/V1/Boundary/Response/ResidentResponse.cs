namespace ElectoralRegisterResidentInformationApi.V1.Boundary.Response
{
    public class ResidentResponse
    {
        /// <example>33798</example>
        public int Id { get; set; }
        /// <example>Ms</example>
        public string Title { get; set; }
        /// <example>Sarah</example>
        public string FirstName { get; set; }
        /// <example>Parker</example>
        public string LastName { get; set; }
        /// <example>Catherine</example>
        public string MiddleName { get; set; }
        /// <example>1969-02-15</example>
        public string DateOfBirth { get; set; }
        /// <example>sarah-p@email.com</example>
        public string Email { get; set; }
        /// <example>British</example>
        public string Nationality { get; set; }
        /// <example>2646214</example>
        public int? Uprn { get; set; }
    }
}
