using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
using EstateExplorer.Core;

namespace EstateExplorer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class NekretninaController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public NekretninaController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            Context = dbcontext;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [HttpGet]
        [Route("PreuzmiNekretninePoParametruPretrage")]
        public async Task<ActionResult> GetNekretnine([FromQuery] string pretraga)
        {
            pretraga = pretraga.ToLower();
            try
            {
                return new JsonResult(new
                {
                    succeeded = true,
                    nekretnine = await Context.Nekretnine.Select(n => new
                    {
                        Id = n.id,
                        Broj = n.Broj,
                        Povrsina = n.Povrsina,
                        BrojListaNepokretnosti = n.BrojListaNepokretnosti,
                       
                        ApplicationUser = n.ApplicationUser
                    })
                 .Where(n => n.Broj.Equals(pretraga) || n.ApplicationUser.JMBG.Contains(pretraga) || n.Povrsina.Equals(pretraga))
                 .ToListAsync()
                });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route("VratiNekretnineKorisnika")]
        public async Task<IActionResult> VratiNekretnineKorisnika ()
        {
            try
            { 
                var user = await SignInManager.UserManager.GetUserAsync(HttpContext.User);
            
                var roles = await SignInManager.UserManager.GetRolesAsync(user);

                if (roles.Count > 0)
                {
                    var userRole = roles[0];
                    if(userRole=="Kupac")
                    {
                     var nekretnine = await Context.Nekretnine
                    .Where(n => n.ApplicationUser.Id== user.Id)
                    .ToListAsync();
                        return Ok(nekretnine);
                    }
                    else { return BadRequest(); }
                }
                else
                     return BadRequest("Niste kupac!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Greška: {ex}");

            }
        }
        [HttpPut]
        [Route("DodeljivanjeKupca/{idNekretnine}")]
        public async Task<ActionResult> DodeljivanjeKupca(int idNekretnine, [FromBody]ApplicationUser a)
        {
            try
            {
                var stara = await Context.Nekretnine.FindAsync(idNekretnine);
                var kupac = await Context.ApplicationUsers.FindAsync(a.Id);

                if (stara != null && kupac != null)
                {
                    stara.ApplicationUser = kupac;
                }
                else
                    return BadRequest("Nepostojeci kupac ili nekretnina");

                await Context.SaveChangesAsync();
                return Ok($"izmenjena nekretnina");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut]
        [Route("AzurirajNekretninu")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
        public async Task<ActionResult> AzurirajNekretninu([FromBody]Nekretnina nek)
        {
            var nekretnina = await Context.Nekretnine.FindAsync(nek.id);
            if (nekretnina == null)
            {
                return NotFound();

            }
            if(nek.BrojListaNepokretnosti!=0)
            nekretnina.BrojListaNepokretnosti = nek.BrojListaNepokretnosti;
            if(nek.Povrsina!=0)
            nekretnina.Povrsina = nek.Povrsina;
            if(nek.Broj!=0)
            nekretnina.Broj = nek.Broj;
          
           
            try
            {
                await Context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost]
        [Route("DodajNekretninu")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
        public async Task<IActionResult> DodajNekretninu(  [FromBody] Nekretnina n)
        {
            try
            {
              
                var nova = new Nekretnina
                {
                    Broj = n.Broj,
                    Povrsina = n.Povrsina,
                    BrojListaNepokretnosti = n.BrojListaNepokretnosti,
                    

                };
                await Context.Nekretnine.AddAsync(nova);
                await Context.SaveChangesAsync();
                return Ok($"ID dodate nekretnine je: {nova.id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("ObrisiNekretninu/{Id}")]
        [HttpDelete]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
        public async Task<ActionResult> ObrisiNekretninu(int Id)
        {
            var nekretnina = await Context.Nekretnine.FindAsync(Id);

            if (nekretnina == null)
            {
                return BadRequest("Ne postoji nekretnina sa zadatim Id-jem"); 
            }
            try
            {
                Context.Nekretnine.Remove(nekretnina);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 
            return Ok($"Uspesno brisanje nekretnine sa ID:{Id} ");
        }

        [HttpPut]
        [Route("{nekretninaId}/postImageNekr")]
        [Authorize(Roles = $"{Constants.Roles.AdministrativniRadnik}")]
        public async Task<IActionResult> PostImageZgrada([FromForm] IFormFile picture, int nekretninaId)
        {
            try
            {
                var n = await Context.Nekretnine.FindAsync(nekretninaId);
                if (n == null)
                    return NotFound();

                using var image = Image.Load(picture.OpenReadStream());
                image.Mutate(x => x.Resize(240, 170));
                var encoder = new JpegEncoder();

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, encoder);
                    n.Image = memoryStream.ToArray();
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
        [Route("getImageNekr/{nId}")]
        public async Task<IActionResult> GetImageZgrada(int nId)
        {

            var nekretnina = await Context.Zgrade.FindAsync(nId);
            return Ok(nekretnina?.Image);
        }
    }
}
