using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class CitiesController : ControllerBase
    {

        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetCities")]
        public async Task<ActionResult<List<string>>> GetCities(CancellationToken cancelationToken)
        {
            var cities = new List<string> { "Vilnius", "Kaunas","Klaipëda"};
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (cancelationToken.IsCancellationRequested)
                    {
                        cities.Add($"cities operation was canceled... Iteration:{i}");
                        break;
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                cities.Add(ex.ToString());
            }
            System.IO.File.AppendAllText("citiesLog.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + JsonSerializer.Serialize(cities) + Environment.NewLine);
            
            return cities;
        }

    }
}