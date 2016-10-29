using System.Collections.Generic;
using System.Threading.Tasks;
using PortableBitBucketClient.V1.Pocos;

namespace PortableBitBucketClient.V1.EndPoints{
    /// <summary>
    /// The issues resource provides functionality for getting information on issues in an issue tracker, 
    /// creating new issues, updating them and deleting them. 
    /// You can access public issues without authentication, but you will only receive a subset of information, 
    /// and you can't gain access to private repositories' issues. By authenticating, you will get a more detailed set of information, 
    /// the ability to create issues, as well as access to updating data or deleting issues you have access to.
    /// </summary>
    public class IssuesResource{
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        #region Issues Resource

        public IssuesResource(RepositoriesEndPoint repositoriesEndPoint){
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Gets the list of issues in the repository.
        /// If you issue this call without filtering parameters, the count value contains the total number of issues in the repository's tracker.  
        /// If you filter this call, the count value contains the total number of issues that meet the filter criteria.
        /// </summary>
        /// <returns></returns>
        public async Task<IssuesInfo> ListIssues(){
            return await _repositoriesEndPoint.ListIssues();
        }

        /// <summary>
        /// Gets in individual issue from a repository. 
        /// Authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        public async Task<Issue> GetIssue(int? issueId){
            return await _repositoriesEndPoint.GetIssue(issueId);
        }

        /// <summary>
        /// Creates a new issue in a repository. This call requires authentication. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// The authenticated user is used for the issue's reported_by field.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public async Task<Issue> PostIssue(Issue issue){
            return await _repositoriesEndPoint.PostIssue(issue);
        }

        /// <summary>
        /// Updates an existing issue. Updating the title or content fields requires that the caller authenticate as a user with write access. 
        /// For other fields, the caller must authenticate as a user with read access. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public async Task<Issue> PutIssue(Issue issue){
            return await _repositoriesEndPoint.PutIssue(issue);
        }

        /// <summary>
        /// Deletes the specified issue_id. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public async Task<Issue> DeleteIssue(int? issueId){
            return await _repositoriesEndPoint.DeleteIssue(issueId);
        }

        /// <summary>
        /// List the components associated with the issue tracker. 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Component>> ListComponents(){
            return await _repositoriesEndPoint.ListComponents();
        }

        /// <summary>
        /// Gets an individual component in an issue tracker. 
        /// To get a component, private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The component identifier.</param>
        /// <returns></returns>
        public async Task<Component> GetComponent(int? componentId){
            return await _repositoriesEndPoint.GetComponent(componentId);
        }

        /// <summary>
        /// Creates a new component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return await value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Component> PostComponent(Component component){
            return await _repositoriesEndPoint.PostComponent(component);
        }

        /// <summary>
        /// Updates an existing component in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Component> PutComponent(Component component){
            return await _repositoriesEndPoint.PutComponent(component);
        }

        /// <summary>
        /// Deletes a component in an issue tracker. Keep in mind that the component can be in use on existing issues. 
        /// To delete a component, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="componentId">The component identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Component> DeleteComponent(int? componentId){
            return await _repositoriesEndPoint.DeleteComponent(componentId);
        }

        /// <summary>
        /// List all the versions associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Version>> ListVersions(){
            return await _repositoriesEndPoint.ListVersions();
        }

        /// <summary>
        /// Gets an individual version in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The version identifier.</param>
        /// <returns></returns>
        public async Task<Version> GetVersion(int? versionId){
            return await _repositoriesEndPoint.GetVersion(versionId);
        }

        /// <summary>
        /// Creates a new version in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return await value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Version> PostVersion(Version version){
            return await _repositoriesEndPoint.PostVersion(version);
        }

        /// <summary>
        /// Updates an existing version in an issue tracker. You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Version> PutVersion(Version version){
            return await _repositoriesEndPoint.PutVersion(version);
        }

        /// <summary>
        /// Deletes a version in an issue tracker. Keep in mind that the version can be in use on existing issues. 
        /// To delete a version, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="versionId">The version identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Version> DeleteVersion(int? versionId){
            return await _repositoriesEndPoint.DeleteVersion(versionId);
        }

        /// <summary>
        /// List all the milestones associated with the issue tracker.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Milestone>> ListMilestones(){
            return await _repositoriesEndPoint.ListMilestones();
        }

        /// <summary>
        /// Gets an individual milestone in an issue tracker. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestoneId">The milestone identifier.</param>
        /// <returns></returns>
        public async Task<Milestone> GetMilestone(int? milestoneId){
            return await _repositoriesEndPoint.GetMilestone(milestoneId);
        }

        /// <summary>
        /// Creates a new milestone in an issue tracker. You must supply a name value in the form of a string. 
        /// The server creates the id for you and it appears in the return await value. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestone">The milestone.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Milestone> PostMilestone(Milestone milestone){
            return await _repositoriesEndPoint.PostMilestone(milestone);
        }

        /// <summary>
        /// Updates an existing milestone in an issue tracker. 
        /// You must supply a name value in the form of a string. 
        /// Public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation.
        /// </summary>
        /// <param name="milestone">The milestone.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Milestone> PutMilestone(Milestone milestone){
            return await _repositoriesEndPoint.PutMilestone(milestone);
        }

        /// <summary>
        /// Deletes a milestone in an issue tracker. Keep in mind that the milestone can be in use on existing issues. 
        /// To delete a milestone, public and private issue trackers require the caller to authenticate with an account that has appropriate authorisation. 
        /// </summary>
        /// <param name="milestoneId">The milestone identifier.</param>
        /// <returns>The response from the BitBucket API.</returns>
        public async Task<Milestone> DeleteMilestone(int? milestoneId){
            return await _repositoriesEndPoint.DeleteMilestone(milestoneId);
        }

        #endregion

        #region Issue Resource

        /// <summary>
        /// Get the issue resource.
        /// BitBucket does not have this resource so this is a "virtual" resource
        /// which offers easier access manipulation of the specified issue.
        /// </summary>
        /// <param name="issueId">The Id of the issue whose resource you wish to get.</param>
        /// <returns></returns>
        public IssueResource IssueResource(int issueId){
            return new IssueResource(_repositoriesEndPoint, issueId);
        }

        internal async Task<List<Comment>> ListIssueComments(Issue issue){
            return await _repositoriesEndPoint.ListIssueComments(issue);
        }

        /// <summary>
        /// List all the comments on the specified issue. 
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <returns></returns>
        internal async Task<List<Comment>> ListIssueComments(int issueId){
            return await _repositoriesEndPoint.ListIssueComments(issueId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        internal async Task<Comment> GetIssueComment(Issue issue, int? commentId){
            return await _repositoriesEndPoint.GetIssueComment(issue, commentId);
        }

        /// <summary>
        /// Get a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        internal async Task<Comment> GetIssueComment(int issueId, int? commentId){
            return await _repositoriesEndPoint.GetIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Response from the BitBucket API.</returns>
        internal async Task<Comment> PostIssueComment(Issue issue, Comment comment){
            return await _repositoriesEndPoint.PostIssueComment(issue, comment);
        }

        /// <summary>
        /// Post a comment to the selected issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Response from the BitBucket API.</returns>
        internal async Task<Comment> PostIssueComment(int issueId, Comment comment){
            return await _repositoriesEndPoint.PostIssueComment(issueId, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> PutIssueComment(Issue issue, Comment comment){
            return await _repositoriesEndPoint.PutIssueComment(issue, comment);
        }

        /// <summary>
        /// Update a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> PutIssueComment(int issueId, Comment comment){
            return await _repositoriesEndPoint.PutIssueComment(issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> DeleteIssueComment(Issue issue, Comment comment){
            return await _repositoriesEndPoint.DeleteIssueComment(issue, comment);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> DeleteIssueComment(Issue issue, int? commentId){
            return await _repositoriesEndPoint.DeleteIssueComment(issue, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> DeleteIssueComment(int? issueId, int? commentId){
            return await _repositoriesEndPoint.DeleteIssueComment(issueId, commentId);
        }

        /// <summary>
        /// Delete a specific comment of an issue.
        /// </summary>
        /// <param name="issueId">The issue identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>The response of BitBucket API.</returns>
        internal async Task<Comment> DeleteIssueComment(int? issueId, Comment comment){
            return await _repositoriesEndPoint.DeleteIssueComment(issueId, comment);
        }

        #endregion
    }
}