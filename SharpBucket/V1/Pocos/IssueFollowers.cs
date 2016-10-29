using System.Collections.Generic;

namespace PortableBitBucketClient.V1.Pocos
{
	public class IssueFollowers
	{
		public int Count { get; set; }
		public List<User> Followers { get; set; }
	}
}