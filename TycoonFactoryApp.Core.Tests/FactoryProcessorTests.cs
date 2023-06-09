namespace TycoonFactoryApp.Core.Tests
{
    public class ActivityRequestProcessorTests
    {
        private readonly FactoryProcessor _processor;
        private ActivityCreateRequestDto _request;       
        private ActivityResponseDto _existingActivity;
        private Mock<IActivityService> _activityServiceMock;
        private Mock<IAndroidWorkerService> _androidWorkerServiceMock;      
        public ActivityRequestProcessorTests()
        {
            //Arrange               
            _request = new ActivityCreateRequestDto
            {  
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Type = ActivityType.BuildComponent,
                Workers = new List<char>
                {
                    'A'
                }
            };           
            _existingActivity = new ActivityResponseDto
            {
                Id = 1,              
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Type = ActivityType.BuildComponent,
                Workers = new List<char>
                {
                    'A',
                }
            };

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ActivityProfile>();
            });
            _activityServiceMock = new Mock<IActivityService>();
            _androidWorkerServiceMock = new Mock<IAndroidWorkerService>();
            _processor = new FactoryProcessor(_activityServiceMock.Object, _androidWorkerServiceMock.Object);
        }
       
        [Fact]
        public async Task Should_Return_Activity_Scheduled_Response_With_Values()
        {
            // Arrange
            _activityServiceMock.Setup(q => q.GetActivityById(_existingActivity.Id)).ReturnsAsync(_existingActivity);

            var expectedResponse = new ActivityUpdateResultDto
            {
                Activity = new ActivityResponseDto { StartDate = _request.StartDate, EndDate = _request.EndDate, Id = 1, Type = _request.Type, Workers = _request.Workers},
            };
            // Act
            ActivityCreationResultDto result = await _processor.CreateActivityAsync(_request);

            // Assert           
            result.ShouldNotBeNull();          
            result.Activity.StartDate.ShouldBe(expectedResponse.Activity.StartDate);
            result.Activity.EndDate.ShouldBe(expectedResponse.Activity.EndDate);
            result.Activity.Type.ShouldBe(expectedResponse.Activity.Type);
            result.Activity.Workers.ShouldBe(expectedResponse.Activity.Workers);
        }

        [Fact]
        public async Task Should_Response_Be_Not_Success_For_Null_Request()
        {
            //Arrange
            var expectedResponse = new ActivityCreationResultDto
            {
               Activity = ActivityResponseDto.Empty,
               Message = "Empty request"
            };
           
            //Act
            var actualResponse = await _processor.CreateActivityAsync(null);

            //Assert           
            actualResponse.ShouldNotBeNull();
            actualResponse.Activity.ShouldBeEquivalentTo(expectedResponse.Activity);
            actualResponse.Message.ShouldBe(expectedResponse.Message);           
        }

        [Theory]
        [InlineData(new char[] { 'A' }, "2023-03-15T09:00:00", "2023-03-15T11:00:00")]
        [InlineData(new char[] { 'B' }, "2023-03-16T08:00:00", "2023-03-16T12:00:00")]
        [InlineData(new char[] { 'C' }, "2023-03-17T10:00:00", "2023-03-17T12:00:00")]
        public async Task Should_Return_Conflict_Response_When_Worker_Has_Conflicting_Activity(char[] workerIds, DateTime startDate, DateTime endDate)
        {
            // Arrange        
            var conflictingActivity = new ActivityResponseDto
            {
                Id = 2,               
                StartDate = startDate,
                EndDate = endDate,
                Type = ActivityType.BuildMachine,
                Workers = workerIds.Select(id => id).ToList()
            };

            foreach (var workerId in workerIds)
            {
                var worker = _androidWorkerServiceMock.Object.GetWorkerById(workerId);
                _androidWorkerServiceMock.Setup(q => q.GetActivitiesByWorker(workerId)).ReturnsAsync(new List<ActivityResponseDto> { conflictingActivity });
                _androidWorkerServiceMock.Setup(q => q.GetWorkerById(workerId)).Returns(worker);
            }

            var request = new ActivityCreateRequestDto
            {              
                StartDate = startDate.AddDays(-1),
                EndDate = endDate.AddDays(1),
                Type = ActivityType.BuildMachine,
                Workers = workerIds.Select(id => id).ToList()
            };

            var expectedResponse = new ActivityCreationResultDto
            {       
                Activity = ActivityResponseDto.Empty,
                Message =  "Selected android workers are not available during the specified time period."            
            };

            // Act
            var actualResponse = await _processor.CreateActivityAsync(request);

            // Assert
            actualResponse.ShouldNotBeNull();
            actualResponse.ShouldNotBeNull();
            actualResponse.Activity.ShouldBeEquivalentTo(expectedResponse.Activity);
            actualResponse.Message.ShouldBe(expectedResponse.Message);
        }

        [Fact]
        public async Task Should_Return_Activity_Modified_Response_With_Values()
        {
            // Arrange    
            _activityServiceMock.Setup(q => q.GetActivityById(_existingActivity.Id)).ReturnsAsync(_existingActivity);

            var modifiedRequest = new ActivityUpdateRequestDto
            {  
                Id = 1,                     
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)   
            };

            var expectedResponse = new ActivityUpdateResultDto
            {
                Activity = _existingActivity,               
            };

            // Act
            var actualResponse = await _processor.UpdateActivityAsync(modifiedRequest);

            // Assert
            actualResponse.ShouldNotBeNull();
            actualResponse.Message.ShouldBeEmpty();
            actualResponse.Activity.ShouldBeEquivalentTo(expectedResponse.Activity);          

            _existingActivity.StartDate.ShouldBe(modifiedRequest.StartDate);
            _existingActivity.EndDate.ShouldBe(modifiedRequest.EndDate);
        }

        [Fact]
        public async Task Should_Return_Activity_To_Modify_Not_Success()
        {
            // Arrange
            var invalidId = 5;

            var modifiedRequest = new ActivityUpdateRequestDto
            {
                Id = 5,              
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)               
            };
            var expectedResponse = new ActivityCreationResultDto
            {
                Activity = ActivityResponseDto.Empty,
                Message = $"Activity with Id {invalidId} Not Found"
            };

            // Act           
            var actualResponse = await _processor.UpdateActivityAsync(modifiedRequest);

            // Assert
            actualResponse.ShouldNotBeNull();
            actualResponse.Activity.ShouldBeEquivalentTo(expectedResponse.Activity);
            actualResponse.Message.ShouldBeEquivalentTo(expectedResponse.Message);
        }

        [Fact]
        public async Task Should_Return_Activity_Deleted_Response_Success()
        {
            // Arrange            
            _activityServiceMock.Setup(q => q.GetActivityById(_existingActivity.Id)).ReturnsAsync(_existingActivity);

            var expectedResponse = new ActivityDeletionResultDto
            {
                IsSuccess = true               
            };

            // Act
            var actualResponse = await _processor.DeleteActivityAsync(_existingActivity.Id);

            // Assert
            actualResponse.ShouldNotBeNull();
            actualResponse.IsSuccess.Equals(expectedResponse.IsSuccess);
            _activityServiceMock.Verify(q => q.DeleteActivity(_existingActivity.Id), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Activity_Deletion_Response_Not_Success()
        {
            // Arrange
            var invalidId = 99;
            _activityServiceMock.Setup(q => q.GetActivityById(_existingActivity.Id)).ReturnsAsync(_existingActivity);
            var expectedResponse = new ActivityDeletionResultDto
            {
                IsSuccess = false,
                Message = $"Activity with Id {invalidId} Not Found"
            };

            // Act
            var actualResponse = await _processor.DeleteActivityAsync(invalidId);

            // Assert
            actualResponse.ShouldNotBeNull();
            actualResponse.IsSuccess.ShouldBeFalse();
            actualResponse.Message.ShouldBeEquivalentTo(expectedResponse.Message);            
        }

        [Fact]
        public async Task Should_Return_Top_10_Androids_Working_Response_For_Next_7_Days()
        {
            // Arrange
            var androidWorkers = new List<AndroidWorkerDto>();
            for (char c = 'A'; c <= 'L'; c++)
            {
                androidWorkers.Add(new AndroidWorkerDto() { Id = c });
            }
            var startDate = DateTime.Today.AddDays(1);
            var endDate = startDate.AddDays(7);

            var random = new Random();
            foreach (var androidWorker in androidWorkers)
            {
                androidWorker.Activities = new List<ActivityResponseDto>();
                var numActivities = random.Next(5, 15);
                for (int i = 0; i < numActivities; i++)
                {
                    var start = startDate.AddDays(random.Next(7));
                    var end = start.AddHours(random.Next(2, 9));
                    var activityType = random.Next(2) == 0 ? ActivityType.BuildComponent : ActivityType.BuildMachine;
                    var activity = new ActivityResponseDto
                    {  
                        Id = i+1,
                        StartDate = start,
                        EndDate = end,
                        Type = activityType,
                        Workers = new List<char> { androidWorker.Id }
                    };
                    androidWorker.Activities.Add(activity);
                }
            }           

            _androidWorkerServiceMock.Setup(q => q.GetAndroidWorkersByActivities()).ReturnsAsync(androidWorkers);
            //Act
            var actualResponse = await _processor.GetTop10AndroidWorkingAsync();

            // Assert
            actualResponse.ShouldNotBeNull();          
            actualResponse.Count.ShouldBe(10);
        }

        [Fact]
        public async Task Should_Return_All_Activities()
        {
            // Arrange           
            var activities = new List<ActivityResponseDto>
            {
                new ActivityResponseDto { Id = 1, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-4), Type = ActivityType.BuildComponent, Workers = new List<char>() },
                new ActivityResponseDto { Id = 2, StartDate = DateTime.Today.AddDays(-4), EndDate = DateTime.Today.AddDays(-3), Type = ActivityType.BuildMachine, Workers = new List <char>() },
                new ActivityResponseDto { Id = 3,StartDate = DateTime.Today.AddDays(-3), EndDate = DateTime.Today.AddDays(-2), Type = ActivityType.BuildComponent, Workers = new List <char>() },
                new ActivityResponseDto { Id = 4, StartDate = DateTime.Today.AddDays(-2), EndDate = DateTime.Today.AddDays(-1), Type = ActivityType.BuildMachine, Workers = new List <char>() },
                new ActivityResponseDto { Id = 5, StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today, Type = ActivityType.BuildComponent, Workers = new List <char>() }
            };

            _activityServiceMock.Setup(q => q.GetActivities()).ReturnsAsync(activities);  
            // Act
            var result = await _processor.GetAllActivitiesAsync();

            // Assert
            result.ShouldNotBeNull();          
        }

    }
}
