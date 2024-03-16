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
        public DodatnoController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
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
    }
}
