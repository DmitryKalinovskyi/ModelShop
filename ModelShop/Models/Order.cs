using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        //[ForeignKey(nameof(Client))]
        public string ClientID { get; set; }

        public DateTime? CreatedDate { get; set; }

        // reference navigation

        public Client Client { get; set; }

        public ICollection<Order_Model3D> OrderItems { get; set; }
    }
}
