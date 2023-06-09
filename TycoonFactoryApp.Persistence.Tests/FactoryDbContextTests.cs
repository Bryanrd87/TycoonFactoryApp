namespace TycoonFactoryApp.Persistence.Tests
{
    public class FactoryDbContextTests : IDisposable
    {
        private readonly DbContextOptions<FactoryDbContext> _options;        
        private readonly FactoryDbContext _context;

        public FactoryDbContextTests()
        {
            var dbName = Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<FactoryDbContext>()
                .UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;            
            _context = new FactoryDbContext(_options);
            _context.Database.EnsureCreated();
        }

        [Theory]
        [InlineData(ActivityType.BuildMachine, 'A', 'B', 'C')]
        [InlineData(ActivityType.BuildComponent, 'D')]
        public async Task Should_Save_Activity(ActivityType activityType, params char[] workerIds)
        {
            // Arrange
            var activity = new Activity()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ActivityType = activityType
            };
            foreach (var workerId in workerIds)
            {
                var worker1 = new AndroidWorker { Name = workerId };             

                activity.AndroidWorkers.Add(new ActivityWorker { Worker = worker1 });               
            }          

            // Act
            var result = await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            // Assert
            var savedActivity = _context.Activities.Single(a => a.Id == result.Entity.Id);
            activity.StartDate.ShouldBe(savedActivity.StartDate);
            activity.EndDate.ShouldBe(savedActivity.EndDate);
            activity.ActivityType.ShouldBe(savedActivity.ActivityType);
        }

        [Theory]      
        [InlineData(ActivityType.BuildComponent, 'D')]
        public async Task Should_Update_Activity(ActivityType activityType, char workerIds)
        {
            // Arrange
            var activity = new Activity()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ActivityType = activityType
            };
            var worker1 = new AndroidWorker { Name = workerIds };

            activity.AndroidWorkers.Add(new ActivityWorker { Worker = worker1 });

            // Act
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            // Act
            var savedActivity = _context.Activities.Single(a => a.Id == activity.Id);
            savedActivity.StartDate = DateTime.UtcNow.AddDays(3);
            savedActivity.EndDate = DateTime.UtcNow.AddDays(5);
            await _context.SaveChangesAsync();

            // Assert
            var updatedActivity = _context.Activities.Single(a => a.Id == activity.Id);
            updatedActivity.StartDate.ShouldBe(savedActivity.StartDate);
            updatedActivity.EndDate.ShouldBe(savedActivity.EndDate);
        }

        [Fact]
        public async Task Should_Get_All_Activities()
        {
            // Arrange
            var activities = new List<Activity>
            {
                new Activity
                {                   
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(1),
                    ActivityType = ActivityType.BuildMachine,
                    AndroidWorkers = new List<ActivityWorker>
                    {
                        new ActivityWorker { Worker = new AndroidWorker { Name = 'A' } },
                        new ActivityWorker { Worker = new AndroidWorker { Name = 'B' } },
                        new ActivityWorker { Worker = new AndroidWorker { Name = 'C' } },
                    }
                },
                new Activity
                {                   
                    StartDate = DateTime.UtcNow.AddDays(2),
                    EndDate = DateTime.UtcNow.AddDays(3),
                    ActivityType = ActivityType.BuildComponent,
                    AndroidWorkers = new List<ActivityWorker>
                    {
                        new ActivityWorker { Worker = new AndroidWorker { Name = 'D' } }
                    }
                }
            };

            _context.Activities.AddRange(activities);
            await _context.SaveChangesAsync();

            // Act
            var savedActivities = await _context.Activities.ToListAsync();

            // Assert
            savedActivities.Count.ShouldBe(2);           
        }

        [Fact]
        public async Task Should_Delete_Activity_By_Id()
        {
            // Arrange
            var activity = new Activity
            {               
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(4),
                ActivityType = ActivityType.BuildMachine,
                AndroidWorkers = new List<ActivityWorker>
                {
                    new ActivityWorker { Worker = new AndroidWorker { Name = 'A' } },
                    new ActivityWorker { Worker = new AndroidWorker { Name = 'B' } },
                    new ActivityWorker { Worker = new AndroidWorker { Name = 'C' } },
                }
            };

            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            // Act
            var activityId = activity.Id;
            var activityToDelete = await _context.Activities.FindAsync(activityId);
            _context.Activities.Remove(activityToDelete);
            await _context.SaveChangesAsync();

            // Assert
            var deletedActivity = await _context.Activities.FindAsync(activityId);
            deletedActivity.ShouldBeNull();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
