using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iPassport.Api.Configurations
{
    /// <summary>
    /// Migrations Work
    /// </summary>
    public class MigrationsWork : IHostedService
    {
        /// <summary>
        /// Service Provider
        /// </summary>
        readonly IServiceProvider provider;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="provider">Service Provider</param>
        public MigrationsWork(IServiceProvider provider) => this.provider = provider;

        /// <summary>
        /// Start Method
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Operation Result</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = provider.CreateScope();

            using var contextIdentity = scope.ServiceProvider.GetRequiredService<PassportIdentityContext>();
            await contextIdentity.Database.MigrateAsync();

            using var context = scope.ServiceProvider.GetRequiredService<iPassportContext>();
            await context.Database.MigrateAsync();
        }

        /// <summary>
        /// Stop Method
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Operation Result</returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
