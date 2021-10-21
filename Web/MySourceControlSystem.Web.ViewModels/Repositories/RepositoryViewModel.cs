namespace MySourceControlSystem.Web.ViewModels.Repositories
{
    using MySourceControlSystem.Data.Models;

    public class RepositoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public int IssuesCount { get; set; }

        public int PullRequestsCount { get; set; }
    }
}
