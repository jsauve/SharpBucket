using PortableBitBucketClient.V2.Pocos;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PortableBitBucketClient.V2.EndPoints
{
	public class EndPoint
	{
		// vanilla page length in many cases is 10, requiring lots of requests for larger collections
		private const int DEFAULT_PAGE_LEN = 50;
		protected readonly BitBucketClientV2 _bitBucketClientV2;
		protected readonly string _baseUrl;

		public EndPoint(BitBucketClientV2 bitBucketClientV2, string resourcePath)
		{
			_bitBucketClientV2 = bitBucketClientV2;
			_baseUrl = resourcePath;
		}

		/// <summary>
		/// Generator that allows lazy access to paginated resources.
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="overrideUrl"></param>
		/// <param name="pageLen"></param>
		/// <returns></returns>
		/// 
		// TODO: NEED TO MAKE THS RELIABLY ASYNC!!!

		private IEnumerable<List<TValue>> IteratePages<TValue>(string overrideUrl, int pageLen = DEFAULT_PAGE_LEN)
		{
			Debug.Assert(!String.IsNullOrEmpty(overrideUrl));
			Debug.Assert(!overrideUrl.Contains("?"));

			var requestParameters = new Dictionary<string, object> { { "pagelen", pageLen } };
			IteratorBasedPage<TValue> response;
			int page = 1;
			do
			{
				response = _bitBucketClientV2.Get(new IteratorBasedPage<TValue>(), overrideUrl.Replace(BitBucketClientV2.BITBUCKET_URL, ""), _bitBucketClientV2.Token, requestParameters).Result;
				if (response == null) { break; }

				yield return response.values;

				requestParameters["page"] = ++page;
			} while (!String.IsNullOrEmpty(response.next));
		}

		/// <summary>
		/// Returns a list of paginated values.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="overrideUrl">The override URL.</param>
		/// <param name="max">Set to 0 for unlimited size.</param>
		/// <returns></returns>
		/// <exception cref="System.Net.WebException">Thrown when the server fails to respond.</exception>
		protected List<TValue> GetPaginatedValues<TValue>(string overrideUrl, int max = 0)
		{
			bool isMaxConstrained = max > 0;

			int pageLen = (isMaxConstrained && max < DEFAULT_PAGE_LEN) ? max : DEFAULT_PAGE_LEN;

			List<TValue> values = new List<TValue>();

			foreach (var page in IteratePages<TValue>(overrideUrl, pageLen))
			{
				if (page == null) { break; }

				if (isMaxConstrained &&
					values.Count + page.Count >= max)
				{
					values.AddRange(page.GetRange(0, max - values.Count));
					Debug.Assert(values.Count == max);
					break;
				}

				values.AddRange(page);
			}

			return values;
		}
	}
}
