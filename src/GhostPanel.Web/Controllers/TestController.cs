using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("weather")]
        public string GetWeather()
        {
            var client = new HttpClient();
            string response = client.GetStringAsync("https://api.darksky.net/forecast/3efa1f91d0180279de8a5080c418b657/44.5596668,-69.6319608").Result;
            return response;
        }
    }
}