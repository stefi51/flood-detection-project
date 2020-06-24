using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Gateway.Utilities
{
	public class JsonLoader
	{
		public static async Task<T> LoadFromFileAsync<T>(string filePath)
		{
			using (StreamReader reader = new StreamReader(filePath))
			{
				string json = await reader.ReadToEndAsync();
				T result = JsonConvert.DeserializeObject<T>(json);
				return result;
			}
		}

		public static T Deserialize<T>(object jsonObject)
		{
			return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject));
		}

	}
}