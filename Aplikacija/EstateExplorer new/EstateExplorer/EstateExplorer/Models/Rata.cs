using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{
    public class Rata
    {
        [Key]
        public int id { get; set; }
        public double IznosKupac { get; set; }
        public DateTime? DatumKupac { get; set; } 
        public double IznosRadnik { get; set; }
        public DateTime? DatumRadnik { get; set; }

        public double IznosKonacan { get; set; }

        public string Valuta { get; set; } = default!;

        public bool Kes { get; set; }
        public Nekretnina? Nekretnina { get; set; }


    }
}
