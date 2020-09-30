using ElectoralRegisterResidentInformationApi.V1.Boundary.Response;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectoralRegisterResidentInformationApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/residents")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ElectoralRegisterController : BaseController
    {
        private readonly IGetAllResidentsUseCase _getAllResidentsUseCase;
        private readonly IGetResidentByIdUseCase _getResidentByIdUseCase;
        public ElectoralRegisterController(IGetAllResidentsUseCase getAllResidentsUseCase, IGetResidentByIdUseCase getResidentByIdUseCase)
        {
            _getAllResidentsUseCase = getAllResidentsUseCase;
            _getResidentByIdUseCase = getResidentByIdUseCase;
        }

        /// <summary>
        /// Searches residents in the electoral registers.
        /// </summary>
        /// <response code="200">Residents results matching search criteria.</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ResidentInformationList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListResidents()
        {
            return Ok(_getAllResidentsUseCase.Execute());
        }

        /// <summary>
        /// Retrieves a single residents details from the electoral register data set based on the ID given.
        /// </summary>
        /// <response code="200">Returns resident information for the given ID.</response>
        /// <response code="400">Given ID is not a number.</response>
        /// <response code="404">A resident associated to the given ID could not be found in the electoral register data.</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult ViewRecord(int id)
        {
            try
            {
                return Ok(_getResidentByIdUseCase.Execute(id));
            }
            catch (ResidentNotFoundException)
            {
                return NotFound("No record could be found for the provided ID");
            }
        }
    }
}
