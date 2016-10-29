using System.Collections.Generic;

namespace PortableBitBucketClient.V1.Pocos
{
	public class ChangesetInfo
	{
		public int? count { get; set; }
		public object start { get; set; }
		public int? limit { get; set; }
		public List<Changeset> changesets { get; set; }
	}
}