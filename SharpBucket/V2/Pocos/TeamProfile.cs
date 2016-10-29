using System.Collections.Generic;

namespace PortableBitBucketClient.V2.Pocos
{
	public class TeamProfile
	{
		public int? pagelen { get; set; }
		public List<Link> links { get; set; }
		public int? page { get; set; }
		public ulong? size { get; set; }
	}
}