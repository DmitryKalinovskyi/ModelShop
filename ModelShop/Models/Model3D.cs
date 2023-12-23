using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    public class Model3D
    {
        [Key]
        public int Model3DID { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(20)]
        public string? ImageSource { get; set; }

        public decimal Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
        
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("ModelCategoryID")]
        public int? ModelCategoryID { get; set; }

        [ForeignKey("OwnerID")]
        public int? OwnerID { get; set; }

        // reference navigation
        public ModelCategory? ModelCategory { get; set; }

        public Client? Owner { get; set; }

    }
}
