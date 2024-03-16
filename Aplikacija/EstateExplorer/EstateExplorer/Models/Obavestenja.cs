using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{
    public class Obavestenja
    {
        [Key]
        public int id { get; set; }

        public string Naslov { get; set; } = default!;
        public string Tekst { get; set; } = default!;
        public string Tip { get; set; } = default!;

        public DateTime? Datum { get; set; }
        public List<VidjenoObavestenje>? VidjenaObavestenja { get; set; }
    }
}
