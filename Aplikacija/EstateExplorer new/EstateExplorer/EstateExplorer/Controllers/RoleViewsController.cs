using EstateExplorer.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstateExplorer.Controllers
{
    public class RoleViewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = Constants.Policies.RequireAdministrativniRadnik)]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
        public IActionResult AdministrativniRadnik()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdministrator")]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]

        public IActionResult Administrator()
        {
            return View();
        }
    }
}
