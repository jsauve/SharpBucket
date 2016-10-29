using System.Collections.Generic;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;

namespace PortableBitBucketClient.Authentication
{
	internal class RequestExecutor
	{
		internal async static Task<T> ExecuteRequest<T>(string url, HttpMethod method, T body, HttpClient client, string token = null, Dictionary<string, object> requestParameters = null) where T : new()
		{
			HttpResponseMessage httpResponseMessage = null;

			T result = default(T);

			if (requestParameters.Any())
				url = $"{url}?{requestParameters.ToQueryString()}";

			if (!string.IsNullOrWhiteSpace(token))
			{
				if (url.EndsWith("&"))
					url = $"{url}access_token={token}";
				else
					url = $"{url}?access_token={token}";
			}

			url = url.TrimEnd('&');

			try
			{
				switch (method)
				{
				case HttpMethod.GET:
					httpResponseMessage = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
					httpResponseMessage.EnsureSuccessStatusCode();
					result = JsonConvert.DeserializeObject<T>(await httpResponseMessage?.Content.ReadAsStringAsync());
					break;
				case HttpMethod.POST:
					var postContent = new StringContent(JsonConvert.SerializeObject(body));
					postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					httpResponseMessage = await client.PostAsync(url, postContent);
					httpResponseMessage.EnsureSuccessStatusCode();
					break;
				case HttpMethod.PUT:
					var putContent = new StringContent(JsonConvert.SerializeObject(body));
					putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					httpResponseMessage = await client.PutAsync(url, putContent);
					httpResponseMessage.EnsureSuccessStatusCode();
					break;
				case HttpMethod.DELETE:
					httpResponseMessage = await client.DeleteAsync(url);
					httpResponseMessage.EnsureSuccessStatusCode();
					break;
				default:
					throw new ArgumentException("No HttpMethod matched. This should never happen.");
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			// This is a hack in order to allow this method to work for simple types as well
			// one example of this is the GetRevisionRaw method
			if (RequestingSimpleType<T>())
			{
				return (await httpResponseMessage.Content.ReadAsStringAsync()) as dynamic;
			}

			return result;
		}

		private static bool ShouldAddBody(HttpMethod method)
		{
			return method == HttpMethod.PUT || method == HttpMethod.POST;
		}

		private static bool RequestingSimpleType<T>() where T : new()
		{
			return typeof(T) == typeof(object);
		}
	}
}