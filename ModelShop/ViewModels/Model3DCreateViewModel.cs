using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        public int? ModelCategoryID { get; set; }
    }
}
