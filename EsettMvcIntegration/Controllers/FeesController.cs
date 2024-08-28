using Microsoft.AspNetCore.Mvc;
using EsettMvcIntegration.Models;
using EsettMvcIntegration.Services;


namespace EsettMvcIntegration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FeesController : ControllerBase
    {
        private readonly FeeDataRepository _repository;
        private readonly FeeDataService _service;

        public FeesController(FeeDataRepository repository, FeeDataService service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Retrieves all fee data from the database.
        /// </summary>
        /// <response code="200">If the request is successful, returns a list of fee data items with HTTP status 200 OK.</response>
        /// <response code="500">If an error occurs while retrieving the data, returns HTTP status 500 Internal Server Error with an error message.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<FeeDataModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FeeDataModel>>> GetAll()
        {
            try
            {
                var fees = await _repository.GetAllFeesAsync();
                return Ok(fees);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here) and return a failure response.
                return StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message = "An error occurred while retrieving the data." });
            }
        }

        /// <summary>
        /// Retrieves a fee entry by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the fee entry to retrieve.</param>
        /// <returns>
        /// An <see cref="ActionResult{FeeDataModel}"/> that represents the result of the operation. 
        /// <response code="200">Returns the requested fee entry.</response>
        /// <response code="404">If the fee entry with the specified identifier is not found.</response>
        /// <response code="500">If there is an internal server error while processing the request.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FeeDataModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FeeDataModel>> GetById(int id)
        {
            try
            {
                var fee = await _repository.GetFeeByIdAsync(id);
                if (fee == null)
                {
                    return NotFound(new DialengaErrorDTO { Message = "Fee not found." });
                }
                return Ok(fee);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here) and return a failure response.
                return StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message = "An error occurred while retrieving the data." });
            }
        }

        /// <summary>
        /// Fetches fee data from an external API and stores it in the database.
        /// </summary>
        /// <response code="200">If all fee data items are successfully added to the database.</response>
        /// <response code="500">If an error occurs during the process, with an error message in the response body.</response>
        [HttpGet("fetch")]
        [ProducesResponseType(typeof(List<FeeAllDataModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchAndStoreFees()
        {
            try
            {
                var fees = await _service.GetFeesAsync();
                return Ok(fees);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here) and return a failure response.
                return StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message = "An error occurred while processing your request." });
            }
        }
    }
}
