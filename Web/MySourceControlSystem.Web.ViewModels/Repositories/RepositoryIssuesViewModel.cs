namespace MySourceControlSystem.Web.ViewModels.Repositories
{
    using System.Collections.Generic;

    public class RepositoryIssuesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }
    }
}
