using System;
using System.Collections.Generic;
using AnalyticsMicroservice.AServices;
using AnalyticsMicroservice.Infrastructure;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SharedModels;

namespace AnalyticsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IRefinedDataRepository repository { get; set; }
        private AnalyticsService analyticsService;
        public AnalyticsController(IRefinedDataRepository dataRepository, AnalyticsService analyticsService)
        {
            this.repository = dataRepository;
            this.analyticsService = analyticsService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<RefinedData>> GetData()
        {
            return this.repository.GetAll();
        }
        [HttpPost("newSensorData")]
        public ActionResult NewSensorData([FromBody] SensorData sensorData)
        {
            this.analyticsService.ProcessNewData(sensorData);
            return Ok();
        }

/*        [HttpPost("postdata2")]
        public ActionResult PostData2([FromBody]SensorData k)
        {
			if (k.Rainfall > 0.1 && k.WaterLevel > 5)
				//this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
            return Ok();
        }*/

        /*[HttpPost("")]
        public ActionResult PostData(RefinedData k)
        {
            this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
            return Ok();
        }*/
    }
}