using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TycoonFactoryApp.Core.Contracts.Persistence;
using TycoonFactoryApp.Persistence.DatabaseContext;
using TycoonFactoryApp.Persistence.Repositories;

namespace TycoonFactoryApp.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FactoryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FactoryDbContextConnString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IAndroidWorkerRepository, AndroidWorkerRepository>();

            return services;
        }
    }
}
