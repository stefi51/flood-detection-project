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
        public ActionResult<IEnumerable<RefinedData>> GetData()
        {
            return this.repository.GetAll();
        }


        [HttpPost("")]
        public ActionResult PostData(RefinedData k)
        {
            this.repository.InsertData(k);
            return Ok();
        }


    }
}
