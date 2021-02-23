using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iPassport.Api.Configurations
{
    public class MigrationsWork : IHostedService
    {
        readonly IServiceProvider provider;
        public MigrationsWork(IServiceProvider provider) => this.provider = provider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = provider.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<iPassportContext>();
            await context.Database.MigrateAsync();

            using var contextIdentity = scope.ServiceProvider.GetRequiredService<PassportIdentityContext>();
            await contextIdentity.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
