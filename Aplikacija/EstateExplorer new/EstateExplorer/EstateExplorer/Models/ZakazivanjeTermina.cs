using System.ComponentModel.DataAnnotations;

namespace EstateExplorer.Models
{
    public class ZakazivanjeTermina
    {
        [Key]
        public int id { get; set; }

        public DateTime Vreme { get; set; }

        public ApplicationUser ApplicationUser { get; set; } = default!;

    }
}
