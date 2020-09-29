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
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly IGetResidentByIdUseCase _getResidentByIdUseCase;
        public ElectoralRegisterController(IGetAllUseCase getAllUseCase, IGetResidentByIdUseCase getResidentByIdUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getResidentByIdUseCase = getResidentByIdUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ResponseObjectList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts()
        {
            return Ok(_getAllUseCase.Execute());
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
