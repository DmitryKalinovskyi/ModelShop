using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    [PrimaryKey(nameof(CartID), nameof(Model3DID))]
    public class Cart_Model3D
    {
        [ForeignKey(nameof(Cart))]
        public int CartID { get; set; }

        [ForeignKey(nameof(Model3D))]
        public int Model3DID { get; set; }

        // reference navigation
        public Cart Cart { get; set; }

        public Model3D Model3D { get; set; }
    }
}
