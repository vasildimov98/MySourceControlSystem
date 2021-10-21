namespace MySourceControlSystem.Web.ViewModels.Repositories
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRepositoryViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage ="Name too short!")]
        public string Name { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Description too short!")]
        public string Description { get; set; }

        public bool IsPublic { get; set; }
    }
}
