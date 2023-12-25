using Microsoft.EntityFrameworkCore;
using ModelShop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModelShop.ViewModels
{
    public class Model3DEditViewModel
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string? Title { get; set; }

        public IFormFile? Image { get; set; }

        public IFormFile? File { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public int? ModelCategoryID { get; set; }

        public ICollection<ModelCategory>? ModelCategories { get; set; } 
    }
}
