using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ModelShop.Models;
using System.Diagnostics.CodeAnalysis;

namespace ModelShop.ViewModels
{
    public class Model3DCreateViewModel
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string? Title { get; set; }

        public IFormFile? Image { get; set; }

        public decimal Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public IFormFile Model3DFile { get; set; }

        public int? ModelCategoryID { get; set; }

        public ICollection<ModelCategory>? ModelCategories { get; set; }
    }
}
