using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    public class Model3D
    {
        [Key]
        public int Model3DID { get; set; }

        public string Title { get; set; }

        public string? ImageSource { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("ModelCategory")]
        public int ModelCategoryID { get; set; }

        // reference navigation
        public ModelCategory? ModelCategory { get; set; }
    }
}
