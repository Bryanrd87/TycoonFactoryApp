using TycoonFactoryApp.Domain;

namespace TycoonFactoryApp.Core.Contracts.Persistence
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task RemoveActivity(int id);
    }
}
