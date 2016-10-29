using PortableBitBucketClient.V2.Pocos;

namespace PortableBitBucketClient.V1.Pocos
{
	public class RepositoryPrivilegesUser
	{
		public string repo { get; set; }
		public string privilege { get; set; }
		public Owner user { get; set; }
	}
}