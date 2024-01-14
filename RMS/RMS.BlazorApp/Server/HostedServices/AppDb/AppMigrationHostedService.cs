using RMS.BlazorApp.HostedServices.RestaurantDb;

namespace RMS.BlazorApp.HostedServices.AppDb
{
    public class AppMigrationHostedService : IHostedService 
    {
        private readonly IServiceProvider serviceProvider;
        public AppMigrationHostedService(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<MigrationService>();
            await svc.MigrationAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
