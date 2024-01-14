using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.HostedServices.RestaurantDb 
{
    public class ApplyMigrationService
    {
        private readonly RestaurantDbContext db;
        public ApplyMigrationService(RestaurantDbContext db)
        {
            this.db = db;
        }
        public async Task ApplyMigrationAsync()
        {
            if ((await db.Database.GetPendingMigrationsAsync()).Any())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}