using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IRefinedDataRepository repository { get; set; }
    

        public AnalyticsController(IRefinedDataRepository dataRepository)
        {
            this.repository = dataRepository;         

        }

        [HttpGet("")]
        public ActionResult GetData()
        {
            // return this.repository.GetAll();
            this.repository.InsertData(new RefinedData()
            {
                Id = "15",
                Value = 110
            });
            return Ok();
        }


        [HttpPost("")]
        public ActionResult PostData(RefinedData k)
        {
            this.repository.InsertData(k);
            return Ok();
        }


    }
}
