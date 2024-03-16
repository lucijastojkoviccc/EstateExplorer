using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Web;
using Microsoft.AspNetCore.Cors;
using EstateExplorer.Core;

namespace EstateExplorer.Controllers
{
    [ApiController]
    [Route("api")]

    public class ZgradaController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public ZgradaController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
        }
     
        [HttpGet]
        [Route("GetStanoveUZgradi/{idZgrade}")]
        public async Task<ActionResult> GetStanoveUZgradi(int idZgrade)
        {
            try
            {
                var zgrada = await Context.Zgrade.Include(z => z.Nekretnine).FirstOrDefaultAsync(z => z.id == idZgrade);
                if (zgrada == null)
                    return BadRequest("Zgrada ne postoji!");

                var stanovi = await Context.Stanovi
                    .Where(s => s.Zgrada == zgrada)
                   .Select(x => new
                   {
                       id = x.id,
                       Broj = x.Broj,
                       BrojListaNepokretnosti = x.BrojListaNepokretnosti,
                       Povrsina = x.Povrsina,
                       BrojSoba = x.BrojSoba,
                       CenaPoKvadratuBezPDV = x.CenaPoKvadratuBezPDV,
                       Sprat = x.Sprat,
                       BrojUlaza = x.BrojUlaza,
                       Orijentacija = x.Orijentacija,
                       Opis = x.Opis
                   })
                   .ToListAsync();
                return Ok(stanovi);
                      

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpGet]
        [Route("GetZgradePoParametruPretrage")]
        //[Authorize(Roles = "Administrativni radnik, Administrator, Investitor, Potencijalni kupac, Kupac")]
        public async Task<JsonResult> GetZgradePoParametruPretrage([FromQuery] string searchParam)
        {

            searchParam = searchParam.ToLower();
            return new JsonResult(new
            {
                succedeed = true,
                zgrade = await Context.Zgrade.Include(x => x.Nekretnine)
                .Select(z => new 
                { 
                    id = z.id, 
                    Naziv = z.Naziv, 
                    Ulica = z.Ulica,
                    BrojZgrade = z.BrojZgrade,
                    BrojKatastarskeParcele = z.BrojKatastarskeParcele, 
                    Lift = z.Lift, 
                    BrojSpratova = z.BrojSpratova, 
                    Grejanje = z.Grejanje, 
                    Opis = z.Opis, 
                    KatastarskaOpstina = z.KatastarskaOpstina,
                    Stanovi = z.Nekretnine.OfType<Stan>().Select(s => new
                    {
                        id = s.id,
                        Broj = s.Broj,
                        BrojListaNepokretnosti = s.BrojListaNepokretnosti,
                        Povrsina = s.Povrsina,
                        BrojSoba = s.BrojSoba,
                        CenaPoKvadratuBezPDV = s.CenaPoKvadratuBezPDV,
                        Sprat = s.Sprat,
                        BrojUlaza = s.BrojUlaza,
                        Orijentacija = s.Orijentacija,
                        Opis = s.Opis
                    }).ToList()
                })
                .Where(u => u.id.ToString() == searchParam || u.Naziv.ToLower().Contains(searchParam) || u.Ulica.ToLower().Contains(searchParam))
                .ToListAsync()
            });
        }

        
        [HttpGet]
        [Route("zgrade")]
        public async Task<JsonResult> GetSveZgrade()
        {

            try
            {
                var zgrade = await Context.Zgrade.Include(x => x.Nekretnine)
                     .Select(z => new
                     {
                         id = z.id,
                         Naziv = z.Naziv,
                         Adresa = z.Ulica + " " + z.BrojZgrade,
                         BrojKatastarskeParcele = z.BrojKatastarskeParcele,
                         Lift = z.Lift,
                         BrojSpratova = z.BrojSpratova,
                         Grejanje = z.Grejanje,
                         Opis = z.Opis,
                         KatastarskaOpstina = z.KatastarskaOpstina,
                         Stanovi = z.Nekretnine.OfType<Stan>().Select(s => new
                         {
                             id = s.id,
                             Broj = s.Broj,
                             BrojListaNepokretnosti = s.BrojListaNepokretnosti,
                             Povrsina = s.Povrsina,
                             BrojSoba = s.BrojSoba,
                             CenaPoKvadratuBezPDV = s.CenaPoKvadratuBezPDV,
                             Sprat = s.Sprat,
                             BrojUlaza = s.BrojUlaza,
                             Orijentacija = s.Orijentacija,
                             Opis = s.Opis
                         }).ToList(),
                         //kurs = Context.CurrencyValues.Select(y => new { y.ExchangeMiddle }).Single()
                     })
                    .ToListAsync();


                return new JsonResult(new
                {
                    succeeded = true,
                    zgrade
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    succeeded = false,
                    error = e.Message
                });
            }
        }


        [Route("AzurirajZgradu")]
        [HttpPut]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Investitor}, {Constants.Roles.Administrator}")]
        public async Task<ActionResult> AzurirajZgradu([FromBody] Zgrada zgrada)
        {
                var z = await Context.Zgrade.FindAsync(zgrada.id);
                if(z==null)
                {
                    return NotFound();
                }
            z.Naziv = zgrada.Naziv;
         
            z.BrojSpratova = zgrada.BrojSpratova;
         
            try
            {
                await Context.SaveChangesAsync();

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
            
        }
        [Route("DodajStanoveUZgradu/{id}/stanovi")]
        [HttpPut]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Investitor}, {Constants.Roles.Administrator}")]
        public async Task<ActionResult> DodajStanoveUZgradu(int id, [FromBody] List<Stan> stanovi)
        {
            var z = await Context.Zgrade.FindAsync(id);
            if (z == null)
            {
                return BadRequest("Ne postoji zgrada sa zadatim ID-jem");
            }
            foreach (var stan in stanovi)
            {
                stan.Zgrada = z;
            }
            z.Nekretnine = stanovi;
             
            try
            {
                await Context.SaveChangesAsync();
                return Ok("Stanovi su dodati u zgradu!");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }


        [HttpPost]
        [Route("DodajZgradu")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Investitor}, {Constants.Roles.Administrator}")]
        public async Task<IActionResult> DodajZgradu([FromBody] Zgrada z)
        {
            try
            {
                var zgrada = new Zgrada
                {
                    Naziv = z.Naziv,
                    Ulica = z.Ulica,
                    BrojZgrade = z.BrojZgrade,
                    BrojKatastarskeParcele = z.BrojKatastarskeParcele,
                    Lift = z.Lift,
                    BrojSpratova = z.BrojSpratova,
                    Grejanje = z.Grejanje,
                    KatastarskaOpstina = z.KatastarskaOpstina,
                    Opis = z.Opis
                };
                await Context.Zgrade.AddAsync(zgrada);
                await Context.SaveChangesAsync();
                return Ok($"ID dodate zgrade je: {zgrada.id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }


        [Route("ObrisiZgradu/{id}")]
        [HttpDelete]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Investitor}, {Constants.Roles.Administrator}")]
        public async Task<ActionResult> ObrisiZgradu(int id)
        {
            var zgrada = await Context.Zgrade.Include(z => z.Nekretnine).Where(p => p.id == id).FirstAsync();

            if (zgrada == null)
            {
                return NotFound();
            }

            foreach(var s in zgrada.Nekretnine) {
                Context.Stanovi.Remove(s);
            }
            Context.Zgrade.Remove(zgrada);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{zgradaId}/postImageZgrada")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}, {Constants.Roles.Investitor}, {Constants.Roles.Administrator}")]
        public async Task<IActionResult> PostImageZgrada([FromForm] IFormFile picture, int zgradaId, [FromForm] string name)
        {
            try
            {
                var zgrada = await Context.Zgrade.FindAsync(zgradaId);
                if (zgrada == null)
                    return NotFound();

                using var image = Image.Load(picture.OpenReadStream());
                image.Mutate(x => x.Resize(240, 170));
                var encoder = new JpegEncoder();

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, encoder);
                    zgrada.Image = memoryStream.ToArray();
                }
                
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e) 
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        [Route("getImageZgrada/{zgradaId}")]
        public async Task<IActionResult> GetImageZgrada(int zgradaId)
        {
            
            var zgrada = await Context.Zgrade.FindAsync(zgradaId);
            return Ok(zgrada?.Image);
        }



    }
}
