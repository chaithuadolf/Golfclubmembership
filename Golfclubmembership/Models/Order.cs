using System.ComponentModel.DataAnnotations.Schema;

namespace Golfclubmembership.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int pid { get; set; }

        public int Quantity { get; set; }

        public string Address { get; set; }
    }
}
