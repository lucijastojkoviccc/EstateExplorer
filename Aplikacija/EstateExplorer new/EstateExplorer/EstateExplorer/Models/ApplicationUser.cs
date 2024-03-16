using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{


    public class ApplicationUser : IdentityUser
    {
        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public string? JMBG { get; set; }

        public List<Nekretnina>? Nekretnine { get; set; }

        public List<ZakazivanjeTermina>? ZakazaniTermini { get; set;} 

        public List<VidjenoObavestenje>? VidjenaObavestenja { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {

    }

}