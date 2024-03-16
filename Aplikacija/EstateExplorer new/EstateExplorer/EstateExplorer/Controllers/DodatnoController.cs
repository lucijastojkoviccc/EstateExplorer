using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Azure.Messaging;

namespace EstateExplorer.Controllers
{
    [ApiController]
    [Route("api")]
    public class DodatnoController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public DodatnoController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager, SignInManager<ApplicationUser> signInManager)
        {
            Context = dbContext;
            UserMenager = userMenager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("VratiSrednjiKurs")]
        public async Task<ActionResult> VratiSrednjiKurs()
        {
            try
            {
                var srednjiKurs = await Context.CurrencyValues.Select(x => new { rsd4eur = x.ExchangeMiddle }).SingleAsync();
                return Ok(srednjiKurs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom vraćanja kursa: {ex}");
            }
        }
        [HttpGet]
        [Route("pufla")]
        public async Task<IActionResult> GetUserRole()
        {
            // Dobijanje trenutno autentifikovanog korisnika
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            // Dobijanje uloga korisnika
            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            // Ako korisnik ima uloge, vrati prvu ulogu
            if (roles.Count > 0)
            {
                var userRole = roles[0];
                return Ok(userRole);
            }

            // Ako korisnik nema uloge
            return NotFound();
        }
    }
}
