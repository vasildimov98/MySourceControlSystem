namespace MySourceControlSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Repository
    {
        public Repository()
        {
            this.Issues = new HashSet<Issue>();
            this.PullRequests = new HashSet<PullRequest>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }
    }
}
