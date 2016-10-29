using PortableBitBucketClient.V2.EndPoints;

namespace PortableBitBucketClient.V2
{
	/// <summary>
	/// A client for the V2 of the BitBucket API.
	/// You can read more about the V2 of the API here:
	/// https://confluence.atlassian.com/display/BITBUCKET/Version+2
	/// </summary>
	public sealed class BitBucketClientV2 : BitBucketClient
	{
		public const string BITBUCKET_URL = "https://bitbucket.org/api/2.0/";

		public BitBucketClientV2(string token = null) : base(BITBUCKET_URL, token) { }

		/// <summary>
		/// Get the Teams End Point for a specific team.
		/// </summary>
		/// <param name="teamName">The team whose team End Point you wish to get.</param>
		/// <returns></returns>
		public TeamsEndPoint TeamsEndPoint(string teamName)
		{
			return new TeamsEndPoint(this, teamName);
		}

		/// <summary>
		/// Get the Repositories End point.
		/// </summary>
		/// <returns></returns>
		public RepositoriesEndPoint RepositoriesEndPoint()
		{
			return new RepositoriesEndPoint(this);
		}

		/// <summary>
		/// Get the UsersEndPoint End Point.
		/// </summary>
		/// <param name="accountName">The account for which you wish to get the UsersEndPoint End Point.</param>
		/// <returns></returns>
		public UsersEndpoint UsersEndPoint(string accountName)
		{
			return new UsersEndpoint(accountName, this);
		}
	}
}