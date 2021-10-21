namespace MySourceControlSystem.Web.ViewModels.Repositories
{
    public class PullRequestViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public bool IsLoggInUserTheOwner { get; set; }
    }
}