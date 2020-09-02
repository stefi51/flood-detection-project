using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceMicroservice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeviceMicroservice.Models;
using DeviceMicroservice.Repositories;
using Microsoft.Extensions.Hosting;
using SharedModels;

namespace DeviceMicroservice.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private IDataRepository sensorsDataRepository;
        private Sensors sensorsService;
      
        public DevicesController(IDataRepository dataRepository, Sensors sensors)
        {
            sensorsDataRepository = dataRepository;
            sensorsService = sensors;
        }
   


        [HttpGet("")]
        public ActionResult<LiveMetaData> GetLiveData()
        {

            return sensorsService.GetMetaData();

        }
        
        
        [HttpPost("SetPeriodTime")]
      
        public ActionResult SetPeriodTime([FromBody]int newTimeStep)
        {
            this.sensorsService.ChangeTimeStep(newTimeStep);
            return Ok();
        }
        
/*       [Route("sensorData")]
       // [HttpGet("getSensorData")]
        public ActionResult<IEnumerable<SensorData>> GetAllData()
        {
            return sensorsDataRepository.GetData();
        }*/
       
       

    }
}
