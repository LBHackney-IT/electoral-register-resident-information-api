using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectoralRegisterResidentInformationApi.V1.Boundary.Request
{
    public class ListResidentsRequest
    {
        [FromQuery(Name = "first_name")]
        public string FirstName { get; set; }
        [FromQuery(Name = "last_name")]
        public string LastName { get; set; }
        public int Limit { get; set; } = 20;
        public int Cursor { get; set; } = 0;
    }
}
