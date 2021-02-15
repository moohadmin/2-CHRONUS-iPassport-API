using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _logger.LogDebug("NLog injected into WeatherForecastController");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            
            _logger.LogDebug("Begin - WeatherForecastController Get()");
            
            WeatherForecast[] response = null;
            response = new WeatherForecast[3];
            ///Para Exibição de erro não tratado no log
            _ = response.Count();            
            
            try
            {
                var rng = new Random();
                ///Para teste de erro trado
                response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
                
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error - Into Get()");
                return response;
                
            }
            finally
            {
                _logger.LogDebug("End - WeatherForecastController get()");
            }     

        }
    }
}
