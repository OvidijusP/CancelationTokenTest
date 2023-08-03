using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebApiNet48.Controllers
{
    public class CitiesController : ApiController
    {
        public async Task<IList<string>> GetCities(CancellationToken cancelationToken)
        {
            var cities = new List<string> { "Vilnius", "Kaunas", "Klaipėda", ".NET 4.8 Framework" };
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
            System.IO.File.AppendAllText(@"c:\temp\citiesLog.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + JsonConvert.SerializeObject(cities) + Environment.NewLine);

            return cities;
        }
    }
}
