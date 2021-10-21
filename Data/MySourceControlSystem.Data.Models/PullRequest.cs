namespace MySourceControlSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PullRequest
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int RepositoryId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual Repository Repository { get; set; }
    }
}
