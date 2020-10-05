using System.Collections.Generic;

namespace ElectoralRegisterResidentInformationApi.V1.Boundary.Response
{
    public class ResidentInformationList
    {
        public List<ResidentResponse> Residents { get; set; }
        public string NextCursor { get; set; }
    }
}
