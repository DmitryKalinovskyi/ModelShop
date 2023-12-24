using ModelShop.Models;

namespace ModelShop.ViewModels
{
    public class Model3DDetailViewModel
    {
        public Model3D Model3D { get; set; }

        public bool IsOwner { get; set; }

        public bool IsInCart { get; set; }

        public bool IsOwned { get; set; }
    }
}
