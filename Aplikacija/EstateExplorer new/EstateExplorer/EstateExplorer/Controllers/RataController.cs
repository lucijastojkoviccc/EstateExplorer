using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Duende.IdentityServer.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Drawing;
using EstateExplorer.Core;

namespace EstateExplorer.Controllers
{
    public class RataDTO
    {
        public string Valuta { get; set; }
        public string Kes { get; set; }
        public int Iznos { get; set; }
        public RataDTO() { }
    }
    [Authorize]
    [ApiController]
    [Route("api")]
    public class RataController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public RataController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager, SignInManager<ApplicationUser> signInManager)
        {
            Context = dbContext;
            UserMenager = userMenager;
            SignInManager = signInManager;
        }

        [HttpPost]
        [Route("DodajRatu")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
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
                        Status=r.IznosKupac==r.IznosRadnik,
                        ImeKorisnika = r.Nekretnina.ApplicationUser.Ime + r.Nekretnina.ApplicationUser.Prezime
                    })
                    .ToListAsync();
                    

                return Ok(rate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom vracanja rata: {ex}");
               
            }
        }

        [HttpGet]
        [Route("VratiRateRolesUser/{idNekretnine}")]
        public async Task<IActionResult> VratiRatePoUlogamaUser([FromRoute]int idNekretnine)
        {
            var user = await SignInManager.UserManager.GetUserAsync(HttpContext.User);
            var nekr = await Context.Stanovi.FindAsync(idNekretnine);

            var roles = await SignInManager.UserManager.GetRolesAsync(user);

            try
            {
                var rate = await Context.Rate.Where(r => r.Nekretnina.id == idNekretnine).Select(rata => new {
                    rata.id,
                    rata.IznosKupac,
                    rata.DatumKupac,
                    rata.Valuta,
                    rata.Kes,
                    Status = (rata.IznosKonacan != 0) ? 1 : 
                        (rata.IznosRadnik == 0) ? 2 : (rata.IznosKupac == rata.IznosRadnik) ? 1 : 3
                }).ToListAsync();
                
                return Ok(rate);

            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom vracanja rata: {ex}");

            }
            return Ok();
        }
        [HttpGet]
        [Route("VratiRateRoles")]
        public async Task<IActionResult> VratiRatePoUlogama()
        {
            var user = await SignInManager.UserManager.GetUserAsync(HttpContext.User);
            var roles = await SignInManager.UserManager.GetRolesAsync(user);
            try
            {
                if (roles.Count > 0)
                {
                    var userRole = roles[0];
                    if(userRole=="Administrator" || userRole=="AdministrativniRadnik" || userRole=="Investitor")
                    {
                        var x = Context.Rate.Select(r => new
                        {
                            r.id,
                            r.IznosKonacan,
                            r.IznosRadnik,
                            r.Valuta,
                            r.Kes,
                            r.Nekretnina,
                            ImeKorisnika = r.Nekretnina.ApplicationUser.Ime + r.Nekretnina.ApplicationUser.Prezime,
                            Status = (r.IznosKonacan != 0) ? 1 :
                                        (r.IznosRadnik == 0) ? 2 : (r.IznosKupac == r.IznosRadnik) ? 1 : 3

                        }).ToList();
                        return Ok(x);
                    }
                }
                return BadRequest();
              

            }
            catch (Exception ex)
            {
                return BadRequest($"Greška prilikom vracanja rata: {ex}");

            }
          
        }
        [HttpPut]
        [Route("vrednostiOdNadleznih/{id}")]
        public async Task<IActionResult> vrednostiOdNadleznih (int id, [FromBody]RataDTO rata )
        {
            var r = await Context.Rate.FindAsync(id);
            if(r==null)
            {
                return NotFound();
            }
            r.Valuta = rata.Valuta;
            r.Kes = rata.Kes == "Kes" ? true : false;
            var user = await SignInManager.UserManager.GetUserAsync(HttpContext.User);
            var nekr = user.Nekretnine;
            var roles = await SignInManager.UserManager.GetRolesAsync(user);

            if (roles.Count > 0)
            {
                var userRole = roles[0];
                if(userRole=="Investitor")
                {
                    r.IznosKonacan = rata.Iznos;                    
                }
                else if(userRole =="AdministrativniRadnik")
                {
                    r.IznosRadnik = rata.Iznos;
                    r.DatumRadnik = DateTime.Now;
                    if(r.IznosRadnik==r.IznosKupac)
                    {
                        r.IznosKonacan = rata.Iznos;                        
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("uplatiRatu/{idNekretnine}")]
        public async Task<IActionResult> vrednostiOdKupca( int idNekretnine, [FromBody] RataDTO rata)
        {
            var user = await SignInManager.UserManager.GetUserAsync(HttpContext.User);
            var nekr = await Context.Nekretnine.FindAsync(idNekretnine);
            var roles = await SignInManager.UserManager.GetRolesAsync(user);

            if (roles.Count > 0)
            {
                var userRole = roles[0];
                if (userRole == "Kupac")
                {
                    var rt = new Rata
                    {
                        IznosKupac = rata.Iznos,
                        DatumKupac = DateTime.Now,
                        IznosRadnik = 0,
                        DatumRadnik = null, // datum placanja od strane radnika se jos ne zna
                        Valuta = rata.Valuta,
                        Kes = rata.Kes == "Kes" ? true : false,
                        Nekretnina = nekr
                    };
                    await Context.Rate.AddAsync(rt);
                    await Context.SaveChangesAsync();
                    return Ok(rt.IznosKupac);
                }
                else if(userRole == "AdministrativniRadnik") {
                    Rata r = await Context.Rate.FindAsync(idNekretnine);
                    r.IznosRadnik = rata.Iznos;
                    r.DatumRadnik = DateTime.Now;
                    r.Valuta = rata.Valuta;
                    r.Kes = rata.Kes == "Kes" ? true : false;
                    if(r.IznosKupac == r.IznosRadnik) {
                        r.IznosKonacan = rata.Iznos;
                    }
                    await Context.SaveChangesAsync();
                    return Ok(r.IznosRadnik);
                }
                if(userRole == "Investitor") {
                    Rata r = await Context.Rate.FindAsync(idNekretnine);
                    r.IznosKonacan = rata.Iznos;
                    r.Valuta = rata.Valuta;
                    r.Kes = rata.Kes == "Kes" ? true : false;
                    await Context.SaveChangesAsync();
                    return Ok(r.IznosKonacan);
                }
            }
            return BadRequest();

        }
        [HttpPost]
        [Route("ZahtevajIsplatuRate/{iznosKupac}/{valuta}/{kes}/{idNekretnine}")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Kupac}")]
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
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
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
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
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
