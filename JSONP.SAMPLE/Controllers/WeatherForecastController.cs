using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSONP.SAMPLE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JSONP.SAMPLE.Controllers
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
        }

        [HttpGet]
        public void Get(string callback)
        {
            TokenModelJwt tokenModelJwt = new TokenModelJwt()
            {
                Role = "jsonp",
                Uid = 1,
                Work = "dsdf"
            };
            var rng = new Random();
            var date = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            var modlestr = JsonConvert.SerializeObject(date);
            string call = callback + "(" + modlestr + ")";
            Response.WriteAsync(call);
        }
    }
}