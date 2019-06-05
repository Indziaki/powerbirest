using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardPBI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DashboardPBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private PlotModel _model { get; }
        public ValuesController(IConfiguration configuration)
        {
            var settings = configuration.GetSection("AppSettings").Get<AppSettings>();
            _model = new PlotModel(settings);
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _model.GetEmbedReport());
        }
    }
}
