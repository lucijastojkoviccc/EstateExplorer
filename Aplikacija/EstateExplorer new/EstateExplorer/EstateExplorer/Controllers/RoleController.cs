using EstateExplorer.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstateExplorer.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Constants.Roles.AdministrativniRadnik)]
        public IActionResult AdministrativniRadnik()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Administrator()
        {
            return View();
        }
    }
}
