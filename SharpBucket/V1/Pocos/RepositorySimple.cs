using PortableBitBucketClient.V2.Pocos;

namespace PortableBitBucketClient.V1.Pocos
{
	public class RepositorySimple
	{
		public Owner owner { get; set; }
		public string name { get; set; }
		public string slug { get; set; }
	}
}