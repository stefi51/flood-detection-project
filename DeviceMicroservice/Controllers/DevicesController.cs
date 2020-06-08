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

namespace DeviceMicroservice.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private IDataRepository repository { get; set; }
        private  Sensors sensor { get; set; }
      
        public DevicesController(IDataRepository dataRepository, Sensors sens)
        {
            this.repository = dataRepository;
            this.sensor = sens;
            //sensor.korak = 10;
            
        }
   


        [HttpGet("")]
        public ActionResult<IEnumerable<Data>> GetData()
        {
           return this.repository.getData();
       }

   
        [HttpPost("")]
        public ActionResult PostData(int d)
        {
            //this.repository.AddData(d);
            this.sensor.setKorak(10);
            return Ok();
        }

        [Route("/korak/{k}")]
        [HttpGet]
        public ActionResult Korak(String k)
        {
            // this.repository.PromeniKorak(Int32.Parse(k));
             this.sensor.setKorak(10);
            
           // return this.repository.getData();
            return Ok();
       }


    }
}
