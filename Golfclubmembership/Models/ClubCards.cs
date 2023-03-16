using System.ComponentModel.DataAnnotations.Schema;

namespace Golfclubmembership.Models
{
    public class ClubCards
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
