using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.RouterModel
{
	public class Destination
	{
		public string Path { get; set; }
		static HttpClient client = new HttpClient(new HttpClientHandler(), false);
		public Destination(string uri)
		{
			Path = uri;
		}

		private Destination()
		{
			Path = "/";
		}

		public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
		{
			string requestContent;
			using (Stream receiveStream = request.Body)
			{
				using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
				{
					requestContent = await readStream.ReadToEndAsync();
				}
			}

			using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request)))
			{
				newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
				using (var response = await client.SendAsync(newRequest))
				{
					return response;
				}
			}
		}

		private string CreateDestinationUri(HttpRequest request)
		{
			string requestPath = request.Path.ToString();
			string queryString = request.QueryString.ToString();

			string endpoint = "";
			string[] endpointSplit = requestPath.Substring(1).Split('/');

			if (endpointSplit.Length > 1)
				endpoint = endpointSplit[1];

			return Path + endpoint + queryString;
		}

	}
}
