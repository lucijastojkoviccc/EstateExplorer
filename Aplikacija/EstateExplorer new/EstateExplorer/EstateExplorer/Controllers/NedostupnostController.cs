using EstateExplorer.Core;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EstateExplorer.Controllers
{
    [ApiController]
    [Route("api")]
    public class NedostupnostController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public NedostupnostController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
        }
       //[Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Admin}")]

    }
}
