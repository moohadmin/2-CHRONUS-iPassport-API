using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics.CodeAnalysis;

namespace iPassport.Api
{
    /// <summary>
    /// Program
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("IPassportAPI - Init main program");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "IPassportAPI - Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Create Host Builder
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>Host Builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging(logging => logging.ClearProviders())
                        .UseNLog()
#if (!DEBUG)
        .UseKestrel(options =>
                        {
                            options.Limits.MaxRequestBodySize = 10485760;
                        })
#endif
                        ;
                });
    }
}
