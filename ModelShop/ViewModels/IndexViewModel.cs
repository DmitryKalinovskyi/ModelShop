using ModelShop.Models;

namespace ModelShop.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Model3D> Models3D { get; set; }

        public IEnumerable<ModelCategory> ModelCategories { get; set; }

        public string Search { get; set; }
        //public int Page { get; set }
    }
}
