namespace MySourceControlSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MySourceControlSystem.Data;
    using MySourceControlSystem.Data.Models;
    using MySourceControlSystem.Web.ViewModels.Repositories;

    public class RepositoriesController : BaseController
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        public RepositoriesController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
            => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRepositoryViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var repository = new Repository
            {
                Name = input.Name,
                Description = input.Description,
                IsPublic = input.IsPublic,
                UserId = this.userManager.GetUserId(this.User),
            };

            await this.db.Repositories.AddAsync(repository);
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.ByUser));
        }

        [Authorize]
        public async Task<IActionResult> DeleteRepository(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var repository = this.db.Repositories.FirstOrDefault(x => x.Id == id);

            foreach (var issue in this.db.Issues.Where(x => x.RepositoryId == id))
            {
                this.db.Issues.Remove(issue);
            }

            foreach (var pullRequest in this.db.PullRequests.Where(x => x.RepositoryId == id))
            {
                this.db.PullRequests.Remove(pullRequest);
            }

            this.db.Repositories.Remove(repository);

            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.ByUser));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateIssue(string content, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var issue = new Issue
            {
                Content = content,
                RepositoryId = id,
                UserId = this.userManager.GetUserId(this.User),
            };

            await this.db.Issues.AddAsync(issue);
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePullRequest(string content, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var pullRequest = new PullRequest
            {
                Content = content,
                RepositoryId = id,
                UserId = this.userManager.GetUserId(this.User),
            };

            await this.db.PullRequests.AddAsync(pullRequest);
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        public async Task<IActionResult> ByUser()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userRepositories = this.db.Repositories
                .Where(x => x.UserId == user.Id)
                .OrderBy(x => x.IsPublic)
                .Select(x => new UserRepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.IsPublic ? "Public" : "Private",
                })
                .AsEnumerable();

            return this.View(userRepositories);
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var repositoryById = this.db.Repositories
                .Where(x => x.Id == id)
                .Select(x => new RepositoryViewModel
                {
                    Id = id,
                    Name = x.Name,
                    Description = x.Description,
                    UserUserName = x.User.UserName,
                    IssuesCount = x.Issues.Count,
                    PullRequestsCount = x.PullRequests.Count,
                })
                .FirstOrDefault();

            return this.View(repositoryById);
        }

        [Authorize]
        public async Task<IActionResult> IssuesById(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var repositoryIssues = this.db.Repositories
                .Where(x => x.Id == id)
                .Select(x => new RepositoryIssuesViewModel
                {
                    Id = id,
                    Name = x.Name,
                    Issues = x.Issues
                        .Select(x => new IssueViewModel
                        {
                            Id = x.Id,
                            Content = x.Content,
                            UserName = x.User.UserName,
                            IsLoggInUserTheOwner = x.UserId == user.Id,
                        }).AsEnumerable(),
                })
                .FirstOrDefault();

            return this.View(repositoryIssues);
        }

        [Authorize]
        public async Task<IActionResult> DeleteIssue(int repId, int id)
        {
            this.db.Issues.Remove(new Issue { Id = id });
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.IssuesById), new { id = repId });
        }

        [Authorize]
        public async Task<IActionResult> PullRequestsById(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var repositoryIssues = this.db.Repositories
                .Where(x => x.Id == id)
                .Select(x => new RepositoryPullRequstsViewModel
                {
                    Id = id,
                    Name = x.Name,
                    PullRequests = x.PullRequests
                        .Select(x => new PullRequestViewModel
                        {
                            Id = x.Id,
                            Content = x.Content,
                            UserName = x.User.UserName,
                            IsLoggInUserTheOwner = x.User.Id == user.Id,
                        }).AsEnumerable(),
                })
                .FirstOrDefault();

            return this.View(repositoryIssues);
        }

        [Authorize]
        public async Task<IActionResult> DeletePullRequest(int repId, int id)
        {
            this.db.PullRequests.Remove(new PullRequest { Id = id });
            await this.db.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.IssuesById), new { id = repId });
        }
    }
}
