using Microsoft.Extensions.DependencyInjection;
using TycoonFactoryApp.Core.Contracts.DataServices;
using TycoonFactoryApp.Core.Features;
using TycoonFactoryApp.Core.Helpers;
using TycoonFactoryApp.Core.MappingProfile;
using TycoonFactoryApp.Core.Processors;

namespace TycoonFactoryApp.Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServiceRegistration(this IServiceCollection services)
        {
            services.AddTransient<IFactoryProcessor, FactoryProcessor>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IAndroidWorkerService, AndroidWorkerService>();
            services.AddAutoMapper(typeof(ActivityProfile));
            services.AddScoped<ExceptionHandlingMiddleware>();
            return services;
        }
    }
}
