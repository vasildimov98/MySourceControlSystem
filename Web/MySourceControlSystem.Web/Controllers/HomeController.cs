namespace MySourceControlSystem.Web.Controllers
{
    using System.Diagnostics;

    using MySourceControlSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using MySourceControlSystem.Data;
    using MySourceControlSystem.Web.ViewModels.Repositories;
    using System.Linq;
    using System.Collections.Generic;
    using MySourceControlSystem.Data.Models;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = this.db.Repositories
                 .Where(x => x.IsPublic)
                 .Select(x => new RepositoryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     UserUserName = x.User.UserName,
                 }).AsEnumerable();

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
