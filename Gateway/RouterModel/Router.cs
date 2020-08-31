using Gateway.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Gateway.RouterModel
{
	public class Router
	{

		public List<Route> Routes { get; set; }
		public Destination AuthenticationService { get; set; }
		static HttpClient client = new HttpClient(new HttpClientHandler(), false);

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

		public async Task<RouterResponse> RouteRequest(HttpRequest request)
		{
			string path = request.Path.ToString();

			Destination destination;
			try
			{
				destination = Routes.First(r => r.Endpoint.Equals(path)).Destination;
			}
			catch
			{
				return new RouterResponse()
				{
					Response = new HttpResponseMessage(HttpStatusCode.NotFound),
					Content = "The path could not be found"
				};
			}
			return await this.SendRequest(destination, request);
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

		public async Task<RouterResponse> SendRequest(Destination destination, HttpRequest request)
		{
			string requestContent;
			using (Stream receiveStream = request.Body)
			{
				using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
				{
					requestContent = await readStream.ReadToEndAsync();
				}
			}

			using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), this.CreateDestinationUri(destination, request)))
			{
				newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
				using (var response = await client.SendAsync(newRequest))
				{
					return new RouterResponse() 
					{
						Response = response,
						Content = await response.Content.ReadAsStringAsync()
					};
				}
			}
		}

		public string CreateDestinationUri(Destination destination, HttpRequest request)
		{
			string requestPath = request.Path.ToString();
			string queryString = request.QueryString.ToString();

			string endpoint = "";
			string[] endpointSplit = requestPath.Substring(1).Split('/');

			if (endpointSplit.Length > 1)
				endpoint = endpointSplit[1];

			return destination.Path + endpoint + queryString;
		}

	}

	public class RouterResponse
	{
		public HttpResponseMessage Response { get; set; }
		public String Content { get; set; }
	}
}