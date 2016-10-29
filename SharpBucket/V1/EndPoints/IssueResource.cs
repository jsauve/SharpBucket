using System.Collections.Generic;
using System.Threading.Tasks;
using PortableBitBucketClient.V1.Pocos;

namespace PortableBitBucketClient.V1.EndPoints
{
	/// <summary>
	/// A "Virtual" End Point that offers easier manipulation of a specific issue.
	/// </summary>
	public class IssueResource
	{
		private readonly RepositoriesEndPoint _repositoriesEndPoint;
		private readonly int _issueId;

		public IssueResource(RepositoriesEndPoint repositoriesEndPoint, int issueId)
		{
			_issueId = issueId;
			_repositoriesEndPoint = repositoriesEndPoint;
		}

		public async Task<Issue> GetIssue()
		{
			return await _repositoriesEndPoint.GetIssue(_issueId);
		}

		/// <summary>
		/// Gets the followers for an individual issue from a repository. 
		/// authorization is not required for public repositories with a public issue tracker. 
		/// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access.
		/// </summary>
		/// <returns></returns>
		public async Task<IssueFollowers> ListFollowers()
		{
			return await _repositoriesEndPoint.ListIssueFollowers(_issueId);
		}

		/// <summary>
		/// List all the comments of the issue. 
		/// </summary>
		/// <returns></returns>
		public async Task<List<Comment>> ListComments()
		{
			return await _repositoriesEndPoint.ListIssueComments(_issueId);
		}

		/// <summary>
		/// Get a specific comment of the issue.
		/// </summary>
		/// <param name="commentId">The comment identifier.</param>
		/// <returns></returns>
		public async Task<Comment> GetIssueComment(int? commentId)
		{
			return await _repositoriesEndPoint.GetIssueComment(_issueId, commentId);
		}

		/// <summary>
		/// Post a comment to the issue.
		/// </summary>
		/// <param name="comment">The comment you wish to post.</param>
		/// <returns>Response from the BitBucket API.</returns>
		public async Task<Comment> PostComment(Comment comment)
		{
			return await _repositoriesEndPoint.PostIssueComment(_issueId, comment);
		}

		/// <summary>
		/// Update a comment of the current issue.
		/// </summary>
		/// <param name="comment">The comment that you wish to update.</param>
		/// <returns>Response from the BitBucket API.</returns>
		public async Task<Comment> PutIssueComment(Comment comment)
		{
			return await _repositoriesEndPoint.PutIssueComment(_issueId, comment);
		}

		/// <summary>
		/// Delete a specific comment of the issue.
		/// </summary>
		/// <param name="commentId">The comment identifier.</param>
		/// <returns>Response from the BitBucket API.</returns>
		public async Task<Comment> DeleteIssueComment(int? commentId)
		{
			return await _repositoriesEndPoint.DeleteIssueComment(_issueId, commentId);
		}
	}
}