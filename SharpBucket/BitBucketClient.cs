using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PortableBitBucketClient.Authentication;

namespace PortableBitBucketClient
{
	/// <summary>
	/// A client for the BitBucket API. It supports V1 and V2 of the API.
	/// More info:
	/// https://confluence.atlassian.com/display/BITBUCKET/Use+the+Bitbucket+REST+APIs
	/// </summary>
	public class BitBucketClient
	{
		string _BaseUrl;
		string _Token;

		public BitBucketClient(string baseUrl, string token = null)
		{
			_BaseUrl = baseUrl;
			_Token = token;
		}

		public string Token => _Token;

		async Task<T> Send<T>(T body, HttpMethod method, string overrideUrl = null, string token = null, Dictionary<string, object> requestParameters = null) where T : new()
		{
			var relativeUrl = overrideUrl;

			T response;

			try
			{
				var httpClient = new HttpClient() { BaseAddress = new Uri(_BaseUrl) };

				response = await RequestExecutor.ExecuteRequest(overrideUrl, method, body, httpClient, token, requestParameters);
			}
			catch (WebException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);

				response = default(T);
			}

			return response;
		}

		public async Task<T> Get<T>(T body, string overrideUrl, string token = null, Dictionary<string, object> requestParameters = null) where T : new()
		{
			return await Send(body, HttpMethod.GET, overrideUrl, token, requestParameters);
		}

		public async Task<T> Post<T>(T body, string overrideUrl, string token = null) where T : new()
		{
			return await Send(body, HttpMethod.POST, overrideUrl, token);
		}

		public async Task<T> Put<T>(T body, string overrideUrl, string token = null) where T : new()
		{
			return await Send(body, HttpMethod.PUT, overrideUrl, token);
		}

		public async Task<T> Delete<T>(T body, string overrideUrl, string token = null) where T : new()
		{
			return await Send(body, HttpMethod.DELETE, overrideUrl, token);
		}
	}
}