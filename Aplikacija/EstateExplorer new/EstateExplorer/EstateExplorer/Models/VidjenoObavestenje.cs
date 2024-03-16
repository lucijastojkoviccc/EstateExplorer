using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace EstateExplorer.Models
{
    public class VidjenoObavestenje
    {
        [Key]
        public int id { get; set; }
        public bool Vidjeno { get; set; }
        public Obavestenja Obavestenje { get; set; } = default!;
        public ApplicationUser ApplicationUser { get; set; } = default!;
    }
}
