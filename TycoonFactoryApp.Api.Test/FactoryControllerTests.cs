namespace TycoonFactoryApp.Api.Tests.Controllers
{
    public class FactoryControllerTests
    {
        private readonly ILogger<FactoryController> _logger;
        private readonly FactoryController _controller;
        private Mock<IFactoryProcessor> _factoryProcessorMock;
        private readonly Fixture _fixture;

        public FactoryControllerTests()
        {
            _factoryProcessorMock = new Mock<IFactoryProcessor>();
            _logger = new LoggerFactory().CreateLogger<FactoryController>();
            _fixture = new Fixture();
            // Arrange
            _controller = new FactoryController(_factoryProcessorMock.Object, _logger);
        }

        [Fact]
        public async Task Get_Returns_OkObjectResult_With_Correct_APIResponse()
        {
            // Arrange
            var activityResponseDto = new Fixture().Create<ActivityResponseDto>();
            var response = new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new List<ActivityResponseDto>
                { activityResponseDto }
            };

            // Act
            var result = await _controller.Get();            

            // Assert
            result.ShouldBeOfType<ActionResult<APIResponse>>();
           
            var okObjectResult = new OkObjectResult(response);
            okObjectResult.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponse = okObjectResult.Value as APIResponse;
            apiResponse.IsSuccess.ShouldBeTrue();
            apiResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponse.Result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_With_Id_Returns_OkObjectResult_With_Correct_APIResponse()
        {
            // Arrange
            var activityResponseDto = new Fixture().Create<ActivityResponseDto>();
            var expectedActivity = new ActivityResultDto { Activity = activityResponseDto };
            _factoryProcessorMock.Setup(q => q.GetActivityByIdAsync(1)).ReturnsAsync(expectedActivity);

            // Act
            var response = await _controller.Get(1);

            // Assert
            response.Result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)response.Result;
            okObjectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponseResult = (APIResponse)okObjectResult.Value;
            apiResponseResult.IsSuccess.ShouldBe(true);
            apiResponseResult.StatusCode.ShouldBe(HttpStatusCode.OK);
            apiResponseResult.Result.ShouldBe(expectedActivity);
        }

        [Fact]
        public async Task Get_Top_Ten_Workers()
        {
            // Arrange
            var topTenWorkers = _fixture.CreateMany<AndroidWorkerTop10Dto>(10).ToList();
            var expectedResponse = new APIResponse { IsSuccess = true, StatusCode = HttpStatusCode.OK, Result = topTenWorkers };
            _factoryProcessorMock.Setup(fp => fp.GetTop10AndroidWorkingAsync()).ReturnsAsync(topTenWorkers);

            // Act
            var response = await _controller.GetTopTenWorkers();

            // Assert
            response.Result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)response.Result;
            okObjectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponseResult = (APIResponse)okObjectResult.Value;
            apiResponseResult.Result.ShouldBe(expectedResponse.Result);
            _factoryProcessorMock.Verify(fp => fp.GetTop10AndroidWorkingAsync(), Times.Once);
        }

        [Fact]
        public async Task Post_Returns_OkObjectResult_With_Correct_APIResponse()
        {
            // Arrange
            var activityResponseDto = new Fixture().Create<ActivityResponseDto>();
            var activityCreateDto = new ActivityCreateRequestDto
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
                Type = Domain.ActivityType.BuildComponent,
                Workers = new List<char> { 'K' }
            };

            var activity = new ActivityResponseDto { Id = 1, EndDate = activityCreateDto.EndDate, StartDate = activityCreateDto.StartDate, Type = activityCreateDto.Type, Workers = activityCreateDto.Workers };
            var activityCreationResultDto = new ActivityCreationResultDto
            {
                Activity = activity
            };

            var apiResponse = new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                Result = activity
            };

            _factoryProcessorMock.Setup(fp => fp.CreateActivityAsync(activityCreateDto)).ReturnsAsync(activityCreationResultDto);

            // Act
            var response = await _controller.Post(activityCreateDto);

            // Assert
            response.Result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)response.Result;
            okObjectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponseResult = (APIResponse)okObjectResult.Value;
            apiResponseResult.IsSuccess.ShouldBe(true);
            apiResponseResult.StatusCode.ShouldBe(HttpStatusCode.Created);
            apiResponseResult.Result.ShouldBeOfType<ActivityResponseDto>();

        }

        [Fact]
        public async Task Put_Returns_NoContent_When_ActivityUpdateRequestDto_Is_Valid()
        {
            // Arrange
            int id = 1;
            var activityUpdateRequestDto = new ActivityUpdateRequestDto { Id = id, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };
            var expectedActivity = new ActivityResponseDto { Id = 1, EndDate = DateTime.Now.AddHours(2), StartDate = DateTime.Now, Type = Domain.ActivityType.BuildComponent, Workers = new List<char> { 'H' } };
            var apiResponse = new APIResponse
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccess = true,
                Result = activityUpdateRequestDto
            };
            _factoryProcessorMock.Setup(x => x.UpdateActivityAsync(activityUpdateRequestDto)).ReturnsAsync(new ActivityUpdateResultDto { Activity = expectedActivity });           

            // Act
            var response = await _controller.Put(id, activityUpdateRequestDto);

            // Assert
            response.Result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)response.Result;
            okObjectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponseResult = (APIResponse)okObjectResult.Value;
            apiResponseResult.IsSuccess.ShouldBe(true);
            apiResponseResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_WhenIdIsValid_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1;
            var expectedResponse = new ActivityDeletionResultDto { IsSuccess = true };
            _factoryProcessorMock.Setup(fp => fp.DeleteActivityAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _controller.Delete(id);

            // Assert
            response.Result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)response.Result;
            okObjectResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okObjectResult.Value.ShouldBeOfType<APIResponse>();
            var apiResponseResult = (APIResponse)okObjectResult.Value;
            apiResponseResult.IsSuccess.ShouldBe(true);
            apiResponseResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            _factoryProcessorMock.Verify(fp => fp.DeleteActivityAsync(id), Times.Once);
        }
    }
}
