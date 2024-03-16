using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
namespace EstateExplorer.Controllers
{
    [ApiController]
    [Route("api")]
    public class StanController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public StanController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
        }

        [HttpGet]
        [Route("PreuzmiStanovePoParametruPretrage")]
        public async Task<ActionResult> GetStanove([FromQuery] string pretraga)
        {
            pretraga = pretraga.ToLower();
            try
            {
                return new JsonResult(new
                {
                   
                    stanovi = await Context.Stanovi
                    .Select(x => new
                    {
                        id = x.id,
                        Broj=x.Broj,
                        BrojListaNepokretnosti=x.BrojListaNepokretnosti,
                        Povrsina=x.Povrsina,
                        BrojSoba = x.BrojSoba,
                        CenaPoKvadratuBezPDV = x.CenaPoKvadratuBezPDV,
                        Sprat = x.Sprat,
                        BrojUlaza = x.BrojUlaza,
                        Orijentacija = x.Orijentacija,
                        Opis = x.Opis,
                        Zgrada=x.Zgrada
                    })
                    .Where(y => y.BrojSoba.Equals(pretraga) || y.CenaPoKvadratuBezPDV.Equals(pretraga) || y.Sprat.Equals(pretraga)
                    || y.BrojUlaza.Equals(pretraga) || y.Orijentacija.Contains(pretraga))
                    .ToListAsync()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPut]
        [Route("AzurirajStan")]
        public async Task<ActionResult> AzurirajStan([FromBody]Stan s)
        {
            try
            {

                var x = await Context.Stanovi.FindAsync(s.id);
                if (x == null)
                {
                    return BadRequest("Stan sa zadatim ID-jem nije pronadjen");
                }
                if(s.Broj!=0)
                    x.Broj = s.Broj;
                if (s.BrojListaNepokretnosti != 0)
                    x.BrojListaNepokretnosti = s.BrojListaNepokretnosti;
                if(s.Povrsina!=0)
                    x.Povrsina=s.Povrsina;
                if(s.BrojSoba!=0)
                    x.BrojSoba = s.BrojSoba;
                if (s.CenaPoKvadratuBezPDV != 0)
                    x.CenaPoKvadratuBezPDV = s.CenaPoKvadratuBezPDV;
                if (s.Sprat != 0)
                    x.Sprat = s.Sprat;
                if (s.BrojUlaza != null)
                    x.BrojUlaza = s.BrojUlaza;
                if (s.Orijentacija != null)
                    x.Orijentacija = s.Orijentacija;
                if (s.Opis != null)
                    x.Opis = s.Opis;
                if (s.Zgrada != null)
                    x.Zgrada = s.Zgrada;

                await Context.SaveChangesAsync();
                return Ok($"Azuriran stan sa ID-jem {s.id}");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("BrisanjeStana/{id}")]
        public async Task<ActionResult> ObrisiStan(int id)
        {
            var stan = await Context.Stanovi.FindAsync(id);

            if (stan == null)
            {
                return BadRequest("Ne postoji stan sa zadatim Id-jem");
            }
            try
            {
                Context.Stanovi.Remove(stan);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok($"Uspesno brisanje stana sa ID:{id} ");
        }

        [HttpPost]
        [Route("DodajStan/{idZgrade}")]
        public async Task<IActionResult> DodajStan(int idZgrade, [FromBody]Stan s)
        {
            try
            {
                var zgrada = await Context.Zgrade.FindAsync(idZgrade);
                if(zgrada == null) { return BadRequest("Zgrada sa zadatim ID-jem nije pronadjena"); }
                var x = new Stan
                {
                  
                    Broj = s.Broj,               
                    BrojListaNepokretnosti = s.BrojListaNepokretnosti,                
                    Povrsina = s.Povrsina,          
                    BrojSoba = s.BrojSoba,              
                    CenaPoKvadratuBezPDV = s.CenaPoKvadratuBezPDV,
                    Sprat = s.Sprat,
                    BrojUlaza = s.BrojUlaza,
                    Orijentacija = s.Orijentacija,
                    Opis = s.Opis,                
                    Zgrada = s.Zgrada
            };
                await Context.Stanovi.AddAsync(x);
                await Context.SaveChangesAsync();
                return Ok($"ID dodatog stana je: {x.id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniStanUZgradi /{idZgrade}")]
        public async Task<IActionResult> IzmeniStanUZgradi(int idZgrade, [FromBody] Stan s)
        {
            try
            {
                var zgrada = await Context.Zgrade.FindAsync(idZgrade);
                if (zgrada == null) { return BadRequest("Zgrada sa zadatim ID-jem nije pronadjena"); }
                var x = await Context.Stanovi.FindAsync(s.id);
                if (x == null)
                {
                    return BadRequest("Stan sa zadatim ID-jem nije pronadjen");
                }
                if (s.Broj != 0)
                    x.Broj = s.Broj;
                if (s.BrojListaNepokretnosti != 0)
                    x.BrojListaNepokretnosti = s.BrojListaNepokretnosti;
                if (s.Povrsina != 0)
                    x.Povrsina = s.Povrsina;
                if (s.BrojSoba != 0)
                    x.BrojSoba = s.BrojSoba;
                if (s.CenaPoKvadratuBezPDV != 0)
                    x.CenaPoKvadratuBezPDV = s.CenaPoKvadratuBezPDV;
                if (s.Sprat != 0)
                    x.Sprat = s.Sprat;
                if (s.BrojUlaza != null)
                    x.BrojUlaza = s.BrojUlaza;
                if (s.Orijentacija != null)
                    x.Orijentacija = s.Orijentacija;
                if (s.Opis != null)
                    x.Opis = s.Opis;
               
                    x.Zgrada = zgrada;

                await Context.SaveChangesAsync();
                return Ok($"Azuriran stan sa ID-jem {s.id}");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
