using ModelShop.Models;

namespace ModelShop.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Model3D> OwnedModels;
        public IEnumerable<Model3D> OrderedModels;
    }
}
