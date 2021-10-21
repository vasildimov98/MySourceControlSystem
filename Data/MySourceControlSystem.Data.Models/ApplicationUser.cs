// ReSharper disable VirtualMemberCallInConstructor
namespace MySourceControlSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MySourceControlSystem.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Repositories = new HashSet<Repository>();
            this.Issues = new HashSet<Issue>();
            this.PullRequests = new HashSet<PullRequest>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }
    }
}
