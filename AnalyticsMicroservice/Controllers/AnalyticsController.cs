using System;
using System.Collections.Generic;
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
        private IHubContext<NotificationService> hub { get; set; }


        public AnalyticsController(IRefinedDataRepository dataRepository, IHubContext<NotificationService> hub)
        {
            this.repository = dataRepository;
            this.hub = hub;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<RefinedData>> GetData()
        {
            return this.repository.GetAll();
        }
        [HttpPost("")]
        public ActionResult PostData2([FromBody]SensorData k)
        {
            this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
            return Ok();
        }

        /*[HttpPost("")]
        public ActionResult PostData(RefinedData k)
        {
            this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
            return Ok();
        }*/
    }
}