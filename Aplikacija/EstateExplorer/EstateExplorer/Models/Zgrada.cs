using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{
    public class Zgrada
    {
        [Key]
        public int id { get; set; }

        public string Naziv { get; set; } = default!;
        public string Ulica { get; set; } = default!;
        public string BrojZgrade { get; set; } = default!;

        public string BrojKatastarskeParcele { get; set; } = default!;
        public bool Lift { get; set; }

        public int BrojSpratova { get; set; }
        public string Grejanje { get; set; } = default!;
        public byte[]? Image { set; get; }

        public string? Opis { get; set; }
        public string KatastarskaOpstina { get; set; } = default!;
        public List<Nekretnina>? Nekretnine { get; set; }



    }
}
