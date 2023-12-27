using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    [Index(nameof(Title))]
    public class Model3D
    {
        [Key]
        public int Model3DID { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? ImageSource { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
        
        public DateTime? CreatedDate { get; set; }

        public int Views { get; set; } = 0;

        [ForeignKey("ModelCategory")]
        public int? ModelCategoryID { get; set; }

        [ForeignKey("Owner")]
        public string? OwnerID { get; set; }

        public string? FileSource { get; set; }

        // reference navigation
        public ModelCategory? ModelCategory { get; set; }

        public Client? Owner { get; set; }

    }
}
