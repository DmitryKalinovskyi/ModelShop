using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [ForeignKey(nameof(Client))]
        public string ClientID { get; set; }

        // reference navigation

        public Client Client { get; set; }

        public IEnumerable<Cart_Model3D> Cart_Models3D { get; set; }
    }
}
