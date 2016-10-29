using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortableBitBucketClient.V2.Pocos;
using Comment = PortableBitBucketClient.V2.Pocos.Comment;
using Repository = PortableBitBucketClient.V2.Pocos.Repository;

namespace PortableBitBucketClient.V2.EndPoints
{
	/// <summary>
	/// The repositories endpoint has a number of resources you can use to manage repository resources. 
	/// For all repository resources, you supply a  repo_slug that identifies the specific repository.
	/// More info:
	/// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint
	/// </summary>
	public class RepositoriesEndPoint : EndPoint
	{

		#region Repositories End Point

		public RepositoriesEndPoint(BitBucketClientV2 sharpBucketV2)
			: base(sharpBucketV2, "repositories/")
		{
		}

		/// <summary>
		/// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
		/// this method returns a collection containing public and private repositories. 
		/// Otherwise, this method returns a collection of the public repositories. 
		/// </summary>
		/// <param name="accountName">The account whose repositories you wish to get.</param>
		/// <param name="max">The maximum number of items to return. 0 returns all items.</param>
		/// <returns></returns>
		public List<Repository> ListRepositories(string accountName, int max = 0)
		{
			var overrideUrl = _baseUrl + accountName + "/";
			return GetPaginatedValues<Repository>(overrideUrl, max);
		}

		/// <summary>
		/// List of all the public repositories on Bitbucket.  This produces a paginated response. 
		/// Pagination only goes forward (it's not possible to navigate to previous pages) and navigation is done by following the URL for the next page.
		/// The returned repositories are ordered by creation date, oldest repositories first. Only public repositories are returned.
		/// </summary>
		/// <param name="max">The maximum number of items to return. 0 returns all items.</param>
		/// <returns></returns>
		public List<Repository> ListPublicRepositories(int max = 0)
		{
			return GetPaginatedValues<Repository>(_baseUrl, max);
		}

		#endregion

		#region Repository resource

		/// <summary>
		/// Use this resource to get information associated with an individual repository. You can use these calls with public or private repositories. 
		/// Private repositories require the caller to authenticate with an account that has the appropriate authorization.
		/// More info:
		/// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
		/// </summary>
		/// <param name="accountName">The owner of the repository.</param>
		/// <param name="repository">The repository slug.</param>
		/// <returns></returns>
		public RepositoryResource RepositoryResource(string accountName, string repository)
		{
			return new RepositoryResource(accountName, repository, this);
		}

		internal async Task<Repository> GetRepository(string accountName, string repository)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, null);
			return await _bitBucketClientV2.Get(new Repository(), overrideUrl);
		}

		internal async Task<Repository> PutRepository(Repository repo, string accountName, string repository)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, null);
			return await _bitBucketClientV2.Put(repo, overrideUrl);
		}

		internal async Task<Repository> PostRepository(Repository repo, string accountName)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repo.name, null);
			return await _bitBucketClientV2.Post(repo, overrideUrl);
		}


		internal async Task<Repository> DeleteRepository(string accountName, string repository)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, null);
			return await _bitBucketClientV2.Delete(new Repository(), overrideUrl);
		}

		private string GetRepositoryUrl(string accountName, string repository, string append)
		{
			var format = _baseUrl + "{0}/{1}/{2}";
			return string.Format(format, accountName, repository, append);
		}

		internal List<Watcher> ListWatchers(string accountName, string repository, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
			return GetPaginatedValues<Watcher>(overrideUrl, max);
		}

		internal List<Fork> ListForks(string accountName, string repository, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
			return GetPaginatedValues<Fork>(overrideUrl, max);
		}

		#endregion

		#region Pull Requests Resource

		public PullRequestsResource PullReqestsResource(string accountName, string repository)
		{
			return new PullRequestsResource(accountName, repository, this);
		}

		internal List<PullRequest> ListPullRequests(string accountName, string repository, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
			return GetPaginatedValues<PullRequest>(overrideUrl, max);
		}

		internal async Task<PullRequest> PostPullRequest(string accountName, string repository, PullRequest pullRequest)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
			return await _bitBucketClientV2.Post(pullRequest, overrideUrl);
		}

		internal async Task<PullRequest> PutPullRequest(string accountName, string repository, PullRequest pullRequest)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
			return await _bitBucketClientV2.Put(pullRequest, overrideUrl);
		}

		internal List<Activity> GetPullRequestLog(string accountName, string repository, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
			return GetPaginatedValues<Activity>(overrideUrl, max);
		}

		#endregion

		#region Pull Request Resource

		internal async Task<PullRequest> GetPullRequest(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
			return await _bitBucketClientV2.Get(new PullRequest(), overrideUrl);
		}

		internal List<Commit> ListPullRequestCommits(string accountName, string repository, int pullRequestId, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
			return GetPaginatedValues<Commit>(overrideUrl, max);
		}

		internal async Task<PullRequestInfo> ApprovePullRequest(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
			return await _bitBucketClientV2.Post(new PullRequestInfo(), overrideUrl);
		}

		internal async Task<object> RemovePullRequestApproval(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
			return await _bitBucketClientV2.Delete(new PullRequestInfo(), overrideUrl);
		}

		internal async Task<object> GetDiffForPullRequest(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
			return await _bitBucketClientV2.Get(new Object(), overrideUrl);
		}

		internal List<Activity> GetPullRequestActivity(string accountName, string repository, int pullRequestId, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
			return GetPaginatedValues<Activity>(overrideUrl, max);
		}

		internal async Task<Merge> AcceptAndMergePullRequest(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
			return await _bitBucketClientV2.Post(new Merge(), overrideUrl);
		}

		internal async Task<Merge> DeclinePullRequest(string accountName, string repository, int pullRequestId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
			return await _bitBucketClientV2.Get(new Merge(), overrideUrl);
		}

		internal List<Comment> ListPullRequestComments(string accountName, string repository, int pullRequestId, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
			return GetPaginatedValues<Comment>(overrideUrl, max);
		}

		internal async Task<Comment> GetPullRequestComment(string accountName, string repository, int pullRequestId, int commentId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
			return await _bitBucketClientV2.Get(new Comment(), overrideUrl);
		}

		#endregion

		#region Branch Restrictions resource

		internal List<BranchRestriction> ListBranchRestrictions(string accountName, string repository, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
			return GetPaginatedValues<BranchRestriction>(overrideUrl, max);
		}

		internal async Task<BranchRestriction> PostBranchRestriction(string accountName, string repository, BranchRestriction restriction)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
			return await _bitBucketClientV2.Post(restriction, overrideUrl);
		}

		internal async Task<BranchRestriction> GetBranchRestriction(string accountName, string repository, int restrictionId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
			return await _bitBucketClientV2.Get(new BranchRestriction(), overrideUrl);
		}

		internal async Task<BranchRestriction> PutBranchRestriction(string accountName, string repository, BranchRestriction restriction)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restriction.id);
			return await _bitBucketClientV2.Put(restriction, overrideUrl);
		}

		internal async Task<BranchRestriction> DeleteBranchRestriction(string accountName, string repository, int restrictionId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
			return await _bitBucketClientV2.Delete(new BranchRestriction(), overrideUrl);
		}

		#endregion

		#region Diff resource

		internal async Task<object> GetDiff(string accountName, string repository, object options)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
			return await _bitBucketClientV2.Get(new object(), overrideUrl);
		}

		internal async Task<object> GetPatch(string accountName, string repository, object options)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
			return await _bitBucketClientV2.Get(new object(), overrideUrl);
		}

		#endregion

		#region Commits Resource

		internal List<Commit> ListCommits(string accountName, string repository, string branchortag = null, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
			if (!string.IsNullOrEmpty(branchortag))
			{
				overrideUrl += branchortag;
			}
			return GetPaginatedValues<Commit>(overrideUrl, max);
		}

		internal async Task<Commit> GetCommit(string accountName, string repository, string revision)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision);
			return await _bitBucketClientV2.Get(new Commit(), overrideUrl);
		}

		internal List<Comment> ListCommitComments(string accountName, string repository, string revision, int max = 0)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/");
			return GetPaginatedValues<Comment>(overrideUrl, max);
		}

		internal async Task<object> GetCommitComment(string accountName, string repository, string revision, int commentId)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/" + revision + "/" + commentId + "/");
			return await _bitBucketClientV2.Get(new object(), overrideUrl);
		}

		internal async Task<object> ApproveCommit(string accountName, string repository, string revision)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
			return await _bitBucketClientV2.Post(new object(), overrideUrl);
		}

		internal async Task<object> DeleteCommitApproval(string accountName, string repository, string revision)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
			return await _bitBucketClientV2.Delete(new object(), overrideUrl);
		}

		#endregion

		#region Default Reviewer Resource

		internal async Task<object> PutDefaultReviewer(string accountName, string repository, string targetUsername)
		{
			var overrideUrl = GetRepositoryUrl(accountName, repository, "default-reviewers/" + targetUsername);
			return await _bitBucketClientV2.Put(new object(), overrideUrl);
		}

		#endregion
	}
}
