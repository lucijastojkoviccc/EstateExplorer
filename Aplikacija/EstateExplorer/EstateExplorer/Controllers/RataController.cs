using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Duende.IdentityServer.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace EstateExplorer.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    public class RataController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public RataController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
        }

        [HttpPost]
        [Route("DodajRatu")]
        public async Task<IActionResult> DodajRatu([FromBody] Rata r, [FromQuery]int idNekretnine)
        {
            try
            {
                var nekretnina = await Context.Nekretnine.FindAsync(idNekretnine);

                if (nekretnina == null)
                {
                    return BadRequest("Nije pronađena nekretnina sa ovim ID-jem.");
                }

                var rata = new Rata
                {
                    IznosKupac = r.IznosKupac,
                    DatumKupac = null,
                    IznosRadnik = 0,
                    DatumRadnik = null, // datum placanja od strane radnika se jos ne zna
                    Valuta = r.Valuta,
                    Kes = r.Kes,
                    Nekretnina = r.Nekretnina
                };
                await Context.Rate.AddAsync(rata);
                await Context.SaveChangesAsync();
                return Ok($"ID dodate rate je: {rata.id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    
        [HttpGet]
        [Route("VratiRate/{idNekretnine}")]
        public async Task<IActionResult> VratiRate(int idNekretnine)
        {
            try
            {
                var n = await Context.Nekretnine.FindAsync(idNekretnine);

                if (n == null)
                {
                    return BadRequest("Nije pronadjena nekretnina sa ovim id-jem");
                }

                var rate = await Context.Rate
                    .Where(r => r.Nekretnina == n)
                    .Select(r => new {
                        r.id,
                        r.IznosKonacan,
                        r.Nekretnina,
                        ImeKorisnika = r.Nekretnina.ApplicationUser.Ime
                    })
                    .ToListAsync();
                    

                return Ok(rate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom vracanja rata: {ex}");
               
            }
        }

        [HttpPost]
        [Route("ZahtevajIsplatuRate/{iznosKupac}/{valuta}/{kes}/{idNekretnine}")]
        public async Task<IActionResult> ZahtevajIsplatuRate(double iznosKupac, string valuta, bool kes, int idNekretnine)
        {
            var datumKupac = DateTime.Now;

            var nekretnina = await Context.Nekretnine.FindAsync(idNekretnine);

            if(nekretnina == null)
            {
                return BadRequest("Ne postoji nekretnina sa ovim id-jem!");
            }

            Rata novaRata = new Rata()
            {
                IznosKupac = iznosKupac,
                DatumKupac = DateTime.Now,
                IznosRadnik = 0,
                Valuta = "EUR",
                IznosKonacan=iznosKupac,
                Kes = true,
                Nekretnina = nekretnina // gde je nekretnina objekat tipa Nekretnina
            };
            await DodajRatu(novaRata, idNekretnine);

            await Context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        [Route("PotvrdiIsplatuRate/{idRate}/{iznosRadnik}")]
        public async Task<IActionResult> PotvrdiIsplatuRate(int idRate, double iznosRadnik)
        {
            var rata = await Context.Rate.FindAsync(idRate);

            if (rata == null)
            {
                return NotFound();
            }

            rata.DatumRadnik = DateTime.Now;
            rata.IznosRadnik = iznosRadnik;

            rata.IznosKonacan = rata.IznosRadnik;

            await Context.SaveChangesAsync();

            return Ok();
        }

        [Route("ObrisiRatu/{id}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiRatu(int id)
        {
            var rata = await Context.Rate.FindAsync(id);

            if (rata == null)
            {
                return NotFound();
            }

            Context.Rate.Remove(rata);
            await Context.SaveChangesAsync();

            return Ok();
        }



    }
}
