using EstateExplorer.Data;
using EstateExplorer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstateExplorer.Controllers
{
    public class ObavestenjaController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserMenager { get; set; }
        public ObavestenjaController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userMenager)
        {
            Context = dbContext;
            UserMenager = userMenager;
        }

        [HttpPost]
        [Route("DodajObavestenja")]
        public async Task<IActionResult> DodajObavestenja([FromBody] Obavestenja obavestenje)
        {
            if (ModelState.IsValid)
            {
                Context.Obavestenja.Add(obavestenje);
                await Context.SaveChangesAsync();
                return Ok(obavestenje);
            }
            return BadRequest(ModelState);
        }


        [HttpDelete]
        [Route("BrisanjeObavestenja/{id}")]
        public async Task<IActionResult> BrisanjeObavestenja(int id)
        {
            var obavestenje = await Context.Obavestenja.FindAsync(id);
            if (obavestenje == null)
            {
                return NotFound();
            }
            Context.Obavestenja.Remove(obavestenje);
            await Context.SaveChangesAsync();
            return Ok(obavestenje);
        }

        [HttpPut]
        [Route("AzuriranjeObavestenja/{id}")]
        public async Task<IActionResult> AzuriranjeObavestenja(int id, [FromBody] Obavestenja obavestenje)
        {
            if (id != obavestenje.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Context.Entry(obavestenje).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return Ok(obavestenje);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("VracanjeObavestenjaById/{id}")]
        public async Task<IActionResult> VracanjeObavestenjaById(int id)
        {
            var obavestenje = await Context.Obavestenja.FindAsync(id);
            if (obavestenje == null)
            {
                return NotFound();
            }
            return Ok(obavestenje);
        }
    }
}
