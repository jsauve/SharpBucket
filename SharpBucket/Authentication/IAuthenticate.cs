//using System.Collections.Generic;
//using System.Net.Http;

//namespace SharpBucket.Authentication
//{
//	public abstract class Authenticate
//	{
//		protected HttpClient client;

//		public virtual T GetResponse<T>(string url, HttpMethod method, T body, Dictionary<string, object> requestParameters)
//		{
//			var executeMethod = typeof(RequestExecutor).GetMethod("ExecuteRequest");

//			var generic = executeMethod.MakeGenericMethod(typeof(T));

//			return (T)generic.Invoke(this, new object[] { url, method, body, client, requestParameters });
//		}
//	}
//}