namespace MySourceControlSystem.Web.ViewModels.Repositories
{
    using System.Collections.Generic;

    public class RepositoryPullRequstsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<PullRequestViewModel> PullRequests { get; set; }
    }
}
