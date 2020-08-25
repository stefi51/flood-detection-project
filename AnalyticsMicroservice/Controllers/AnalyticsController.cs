using System;
using System.Collections.Generic;
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
		private IHubContext<SignalServer> hub { get; set; }

		public AnalyticsController(IRefinedDataRepository dataRepository, IHubContext<SignalServer> hub)
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
		public ActionResult PostData(RefinedData k)
		{
			// this.repository.InsertData(k);
			this.hub.Clients.All.SendAsync("refinedDataUpdate", k);
			return Ok();
		}


	}
}
