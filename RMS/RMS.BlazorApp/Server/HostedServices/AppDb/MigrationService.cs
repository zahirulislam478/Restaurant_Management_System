using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.HostedServices.AppDb
{
    public class MigrationService
    {
        private readonly AppDbContext db;
        public MigrationService(AppDbContext db)  
        {
            this.db = db;
        }
        public async Task MigrationAsync() 
        {
            if ((await db.Database.GetPendingMigrationsAsync()).Any())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
