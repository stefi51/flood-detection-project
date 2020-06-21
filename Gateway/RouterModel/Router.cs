using Gateway.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Gateway.RouterModel
{
	public class Router
	{

		public List<Route> Routes { get; set; }
		public Destination AuthenticationService { get; set; }


		public Router(string routeConfigFilePath)
		{
			this.Init(routeConfigFilePath);
		}

		public async Task Init(string routeConfigFilePath)
		{
			dynamic router = await JsonLoader.LoadFromFileAsync<dynamic>(routeConfigFilePath);

			Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
			AuthenticationService = JsonLoader.Deserialize<Destination>(Convert.ToString(router.authenticationService));
		}

		public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
		{
			string path = request.Path.ToString();
			string basePath = '/' + path.Split('/')[1];

			Destination destination;
			try
			{
				destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
			}
			catch
			{
				return ConstructErrorMessage("The path could not be found.");
			}
			return await destination.SendRequest(request);
		}

		private HttpResponseMessage ConstructErrorMessage(string error)
		{
			HttpResponseMessage errorMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.NotFound,
				Content = new StringContent(error)
			};
			return errorMessage;
		}

	}
}