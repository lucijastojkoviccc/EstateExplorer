using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{
    public class Nekretnina
    {
        [Key]
        public int id { get; set; }

        public int Broj { get; set; }

        public double Povrsina { get; set; }    

        public int BrojListaNepokretnosti { get; set; }
        public byte[]? Image { set; get; }

        public Zgrada Zgrada { get; set; } = default!;

        public List<Rata>? Rate { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

    }
}