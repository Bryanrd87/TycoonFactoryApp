using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Persistence.DatabaseContext
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext() { }
        public FactoryDbContext(DbContextOptions<FactoryDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<AndroidWorker> AndroidWorkers { get; set; }
        public DbSet<ActivityWorker> ActivitiesWorkers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactoryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityWorker>()
                    .HasKey(bc => new { bc.ActivityId, bc.WorkerId });
            modelBuilder.Entity<ActivityWorker>()
                .HasOne(bc => bc.Activity)
                .WithMany(b => b.AndroidWorkers)
                .HasForeignKey(bc => bc.ActivityId);
            modelBuilder.Entity<ActivityWorker>()
                .HasOne(bc => bc.Worker)
                .WithMany(c => c.Activities)
                .HasForeignKey(bc => bc.WorkerId);

            if (Assembly.GetExecutingAssembly().Location.Contains("Test"))
            {
                return;
            }

            modelBuilder.Entity<AndroidWorker>().HasData(Array.Empty<AndroidWorker>());
            modelBuilder.Entity<Activity>().HasData(Array.Empty<Activity>());
            modelBuilder.Entity<ActivityWorker>().HasData(Array.Empty<ActivityWorker>());

            modelBuilder.Entity<AndroidWorker>().HasData(
                new AndroidWorker { Id = 1, Name = 'A' },
                new AndroidWorker { Id = 2, Name = 'B' },
                new AndroidWorker { Id = 3, Name = 'C' },
                new AndroidWorker { Id = 4, Name = 'D' },
                new AndroidWorker { Id = 5, Name = 'E' },
                new AndroidWorker { Id = 6, Name = 'F' },
                new AndroidWorker { Id = 7, Name = 'G' },
                new AndroidWorker { Id = 8, Name = 'H' },
                new AndroidWorker { Id = 9, Name = 'I' },
                new AndroidWorker { Id = 10, Name = 'J' },
                new AndroidWorker { Id = 11, Name = 'K' },
                new AndroidWorker { Id = 12, Name = 'L' },
                new AndroidWorker { Id = 13, Name = 'M' },
                new AndroidWorker { Id = 14, Name = 'N' },
                new AndroidWorker { Id = 15, Name = 'O' },
                new AndroidWorker { Id = 16, Name = 'P' },
                new AndroidWorker { Id = 17, Name = 'Q' },
                new AndroidWorker { Id = 18, Name = 'R' },
                new AndroidWorker { Id = 19, Name = 'S' },
                new AndroidWorker { Id = 20, Name = 'T' },
                new AndroidWorker { Id = 21, Name = 'U' },
                new AndroidWorker { Id = 22, Name = 'V' },
                new AndroidWorker { Id = 23, Name = 'W' },
                new AndroidWorker { Id = 24, Name = 'X' },
                new AndroidWorker { Id = 25, Name = 'Y' },
                new AndroidWorker { Id = 26, Name = 'Z' }
            );  
            
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2), ActivityType = ActivityType.BuildComponent },
                new Activity { Id = 2, StartDate = DateTime.Now.AddHours(2).AddMinutes(1), EndDate = DateTime.Now.AddHours(4), ActivityType = ActivityType.BuildMachine },
                new Activity { Id = 3, StartDate = DateTime.Now.AddHours(4).AddMinutes(1), EndDate = DateTime.Now.AddHours(7), ActivityType = ActivityType.BuildComponent },
                new Activity { Id = 4, StartDate = DateTime.Now.AddHours(7).AddMinutes(1), EndDate = DateTime.Now.AddHours(9), ActivityType = ActivityType.BuildComponent },
                new Activity { Id = 5, StartDate = DateTime.Now.AddHours(9).AddMinutes(1), EndDate = DateTime.Now.AddHours(11), ActivityType = ActivityType.BuildMachine }
            );

            modelBuilder.Entity<ActivityWorker>().HasData(
                new { ActivityId = 1, WorkerId = 1 },              
                new { ActivityId = 2, WorkerId = 4 },
                new { ActivityId = 2, WorkerId = 5 },
                new { ActivityId = 2, WorkerId = 6 },
                new { ActivityId = 3, WorkerId = 7 },
                new { ActivityId = 4, WorkerId = 8 },
                new { ActivityId = 5, WorkerId = 9 },
                new { ActivityId = 5, WorkerId = 2 }
            );
        }
    }
}

