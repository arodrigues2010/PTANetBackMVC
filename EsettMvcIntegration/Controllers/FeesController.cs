
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

        [HttpGet]
        [ProducesResponseType(typeof(List<FeeDataModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FeeDataModel>>> GetAll()
        {
            var result = await TryGetAllFees();
            return result.IsSuccess ? Ok(result.Value) : StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message = "Fee not found."  });
        }

        private async Task<Result<List<FeeDataModel>>> TryGetAllFees()
        {
            try
            {
                var fees = await _repository.GetAllFeesAsync();
                return Result<List<FeeDataModel>>.Success(fees);
            }
            catch (Exception ex)
            {
                return Result<List<FeeDataModel>>.Failure("An error occurred while retrieving the data.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FeeDataModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FeeDataModel>> GetById(int id)
        {
            var result = await TryGetFeeById(id);
            if (result.IsSuccess)
            {
                return result.Value != null ? Ok(result.Value) : NotFound(new DialengaErrorDTO { Message = "Fee not found." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message ="Fee not found." });
        }

        private async Task<Result<FeeDataModel>> TryGetFeeById(int id)
        {
            try
            {
                var fee = await _repository.GetFeeByIdAsync(id);
                return Result<FeeDataModel>.Success(fee);
            }
            catch (Exception ex)
            {
                return Result<FeeDataModel>.Failure("An error occurred while retrieving the data.");
            }
        }


        [HttpGet("fetch")]
        [ProducesResponseType(typeof(List<FeeAllDataModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DialengaErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchAndStoreFees()
        {
            var result = await TryFetchAndStoreFees();
            return result.IsSuccess ? Ok(result.Value) : StatusCode(StatusCodes.Status500InternalServerError, new DialengaErrorDTO { Message = "Fee not found."  });
        }

        private async Task<Result<List<FeeAllDataModel>>> TryFetchAndStoreFees()
        {
            try
            {
                var fees = await _service.GetFeesAsync();
                return Result<List<FeeAllDataModel>>.Success(fees);
            }
            catch (Exception ex)
            {
                return Result<List<FeeAllDataModel>>.Failure("An error occurred while processing your request.");
            }
        }
    }
}
