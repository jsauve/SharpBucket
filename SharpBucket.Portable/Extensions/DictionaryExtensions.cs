using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PortableBitBucketClient
{
	public static class DictionaryExtensions
	{
		public static IEnumerable<KeyValuePair<string, string>> ToKeyValueStringStringPairs(this Dictionary<string, object> dictionary)
		{
			var result = new List<KeyValuePair<string, string>>();

			foreach (var item in dictionary)
			{
				result.Add(new KeyValuePair<string, string>(item.Key, (string)item.Value));
			}

			return result;
		}

		public static IEnumerable<KeyValuePair<string, string>> ToKeyValueStringJsonPairs(this Dictionary<string, object> dictionary)
		{
			var result = new List<KeyValuePair<string, string>>();

			foreach (var item in dictionary)
			{
				result.Add(new KeyValuePair<string, string>(item.Key, JsonConvert.SerializeObject(item.Value)));
			}

			return result;
		}

		public static string ToQueryString(this Dictionary<string, object> dictionary)
		{
			var builder = new StringBuilder();

			foreach (var item in dictionary)
			{
				builder.AppendFormat($"{item.Key}={(string)item.Value}&");
			}

			return builder.ToString();
		}
	}
}
