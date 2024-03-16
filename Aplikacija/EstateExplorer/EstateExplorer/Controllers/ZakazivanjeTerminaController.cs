using EstateExplorer.Data;
using EstateExplorer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstateExplorer.Controllers
{
    public class ZakazivanjeTerminaController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ZakazivanjeTerminaController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            Context = dbcontext;
            UserManager = userManager;
        }
        [HttpGet]
        [Route("GetTermine")]
        public async Task<ActionResult> GetTermine([FromQuery] string pretraga)
        {
            pretraga = pretraga.ToLower();
            //DateTime date = DateTime.Parse(pretraga);
            try
            {
                return new JsonResult(new
                {
                    succeeded = true,
                    termin = await Context.ZakazivanjeTermina.Select(n => new
                    {
                        Id = n.id,
                        Vreme = n.Vreme,
                        ApplicationUser = n.ApplicationUser
                    })
                .Where(n => n.ApplicationUser.Email.Contains(pretraga))
                .ToListAsync()
                });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        [Route("PromenaZakazanogTermina")]
        public async Task<ActionResult> PromenaZakazanogTermina([FromBody]ZakazivanjeTermina zak)
        {
            try
            {
               
                var zakazani = await Context.ZakazivanjeTermina.FindAsync(zak.id);
                if (zakazani == null) { return BadRequest("Ne postoji termin zakazan u to vreme"); }
                
                if(zak.Vreme!=DateTime.Now)
                    zakazani.Vreme=zak.Vreme;
                if (zak.ApplicationUser != null)
                    zakazani.ApplicationUser = zak.ApplicationUser;
                
                await Context.SaveChangesAsync();
                return Ok($"izmenjeno vreme termina");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpDelete]
        [Route("BrisanjeZakazanogTermina/{id}")]
        public async Task<ActionResult> ObrisiStan(int id)
        {
            var zakazani = await Context.ZakazivanjeTermina.FindAsync(id);
            if (zakazani == null) { return BadRequest("Ne postoji termin zakazan u to vreme"); }
            try
            {
                Context.ZakazivanjeTermina.Remove(zakazani);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok($"Uspesno brisanje zakazanog termina");
        }
        [HttpPost]
        [Route("ZakaziTermin/{idK}")]
        public async Task<IActionResult> ZakaziTermin( int idK, [FromBody]ZakazivanjeTermina z)
        {
            try
            {
                var korisnik = await Context.ApplicationUsers.FindAsync(idK);
                if (korisnik == null) { return BadRequest($"Potrebno je kreirati nalog"); }
                var novi = new ZakazivanjeTermina
                {
                    ApplicationUser = korisnik,
                    Vreme=z.Vreme
                };
                await Context.ZakazivanjeTermina.AddAsync(novi);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno zakazan termin");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
