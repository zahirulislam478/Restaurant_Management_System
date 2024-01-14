using RMS.BlazorApp.Models;
using RMS.BlazorApp.Repositories.Interfaces;

namespace RMS.BlazorApp.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        RestaurantDbContext db;
        public UnitOfWork(RestaurantDbContext db)
        {
            this.db = db;
        }
        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        public IGenericRepo<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(this.db);
        }
    }
}
