using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnalyticsMicroservice.Infrastructure;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AnalyticsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IRefinedDataRepository repository { get; set; }
        private static HttpClient _httpClient;
        private IHubContext<SignalServer> hub { get; set; }


        public AnalyticsController(IRefinedDataRepository dataRepository)
        {
            this.repository = dataRepository;
            this.hub = hub;
            _httpClient = new HttpClient();
            Fja();
        }

        static async void Fja()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5002/api/devices");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<RefinedData>> GetData()
        {
            return this.repository.GetAll();
        }


        [HttpPost("")]
        public ActionResult PostData(RefinedData k)
        {
            this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
            return Ok();
        }
    }
}