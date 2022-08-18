using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace MicServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private IConfiguration _iConfiguration;

        public HealthController(IConfiguration configuration)
        {
            _iConfiguration = configuration;
        }


        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            Console.WriteLine($"This is HealthController  {_iConfiguration["port"]} Invoke {DateTime.Now}");

            return Ok(); 
        }

    }
}
