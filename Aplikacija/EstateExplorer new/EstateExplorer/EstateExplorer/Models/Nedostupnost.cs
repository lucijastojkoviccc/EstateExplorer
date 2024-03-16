using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EstateExplorer.Models
{
    public class Nedostupnost
    {
        [Key]
        public int id { get; set; }
        public DateTime? Od { get; set; }
        public DateTime? Do { get; set; }
        public bool Nedostupan { get; set; }

    }
}
