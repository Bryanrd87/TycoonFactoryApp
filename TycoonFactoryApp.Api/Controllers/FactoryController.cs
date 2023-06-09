using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TycoonFactoryApp.Core.Models;
using TycoonFactoryApp.Core.Processors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TycoonFactoryApp.Api.Controllers
{
    [Route("api/factory")]
    [ApiController]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryProcessor _factoryProcessor;
        private readonly ILogger<FactoryController> _logger;        
        private readonly APIResponse _apiResponse;

        public FactoryController(IFactoryProcessor factoryProcessor, ILogger<FactoryController> logger)
        {
            _factoryProcessor = factoryProcessor;
            _logger = logger;           
            _apiResponse = new();
        }
        // GET: api/<FactoryController>       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                _logger.LogInformation("(iLog) Getting All Activities", "info");
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = await _factoryProcessor.GetAllActivitiesAsync();
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Getting Activities {ex.Message}", "error");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(_apiResponse);
        }

        // GET: api/<FactoryController>       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("gettoptenworkers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetTopTenWorkers()
        {
            try
            {
                _logger.LogInformation("(iLog) Getting Top Ten Workers", "info");
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = await _factoryProcessor.GetTop10AndroidWorkingAsync();
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Getting Top Ten Workers {ex.Message}", "error");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(_apiResponse);
        }

        // GET api/<FactoryController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                _logger.LogInformation($"(iLog) Getting Activity {id}", "info");
                var result = await _factoryProcessor.GetActivityByIdAsync(id);
                if (string.IsNullOrEmpty(result.Message))
                {
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    _apiResponse.IsSuccess = true;
                    _apiResponse.Result = result;
                    return Ok(_apiResponse);
                }
                _apiResponse.ErrorMessage = new List<string> { result == null ? string.Empty : result.Message };
                _apiResponse.StatusCode = HttpStatusCode.NotFound;               
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Getting Activity {ex.Message}", "error");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(_apiResponse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] ActivityCreateRequestDto activityCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (activityCreateDto is null)
                    return BadRequest();

                if (activityCreateDto.EndDate < activityCreateDto.StartDate)
                {
                    ModelState.AddModelError("ErrorMessage", "Start Date cannot be greater than End Date");
                    return BadRequest(ModelState);
                }
                if (activityCreateDto.Type == Domain.ActivityType.BuildComponent && activityCreateDto.Workers.Count > 1)
                {
                    ModelState.AddModelError("ErrorMessage", "Selected Activity Type: Build component can have only one Android worker.");
                    return BadRequest(ModelState);
                }

                _logger.LogInformation($"(iLog) Adding Activity", "info");

                var result = await _factoryProcessor.CreateActivityAsync(activityCreateDto);
                if (string.IsNullOrEmpty(result.Message))
                {
                    _apiResponse.Result = result.Activity;
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = HttpStatusCode.Created;
                    return Ok(_apiResponse);
                }

                _apiResponse.ErrorMessage = new List<string> { result == null ? string.Empty : result.Message };
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Adding Activity {ex.Message}", "error");
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage = new List<string>() { ex.Message };
            }

            return BadRequest(_apiResponse);
        }

        // PUT api/<FactoryController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]        
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] ActivityUpdateRequestDto activityUpdateRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (activityUpdateRequestDto is null)
                    return BadRequest();
                if (id != activityUpdateRequestDto.Id)
                {
                    ModelState.AddModelError("ErrorMessage", "Parameter Id and Id from request Activity are not the same");
                    return BadRequest(ModelState);
                }
                if (activityUpdateRequestDto.EndDate < activityUpdateRequestDto.StartDate)
                {
                    ModelState.AddModelError("ErrorMessage", "Start Date cannot be greater than End Date");
                    return BadRequest(ModelState);
                }

                _logger.LogInformation($"(iLog) Updating Activity {activityUpdateRequestDto.Id}", "info");

                var result = await _factoryProcessor.UpdateActivityAsync(activityUpdateRequestDto);
                if (string.IsNullOrEmpty(result.Message))
                {
                    _apiResponse.Result = result.Activity;
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_apiResponse);
                }
                _apiResponse.ErrorMessage = new List<string> { result.Message };
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Updating Activity {ex.Message}", "error");
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage = new List<string>() { ex.Message };
            }

            return BadRequest(_apiResponse);
        }

        // DELETE api/<FactoryController>/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.ErrorMessage = new List<string> { "Parameter Id cannot be 0" };
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                _logger.LogInformation($"(iLog) Deleting Activity {id}", "info");
                var response = await _factoryProcessor.DeleteActivityAsync(id);
                if (response.IsSuccess)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NoContent;
                    _apiResponse.IsSuccess = true;
                    return Ok(_apiResponse);
                }
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.IsSuccess = true;
                _apiResponse.ErrorMessage
                    = new List<string>() { response.Message };
            }
            catch (Exception ex)
            {
                _logger.LogError($"(iLog) Error Deleting Activity {ex.Message}", "error");
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return BadRequest(_apiResponse);
        }
    }
}
