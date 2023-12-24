using Microsoft.EntityFrameworkCore;

namespace ModelShop.Models
{
    [PrimaryKey(nameof(OrderID), nameof(Model3DID))]
    public class Order_Model3D
    {
        public int OrderID { get; set; }

        public int Model3DID { get; set; }

        // reference navigation

        public Order Order { get; set; }

        public Model3D Model3D { get; set; }
    }
}
