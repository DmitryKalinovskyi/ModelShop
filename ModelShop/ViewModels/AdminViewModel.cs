using ModelShop.Models;

namespace ModelShop.ViewModels
{
    public class AdminViewModel
    {
        public ICollection<Order>? Orders { get; set; } 

        //public int PageSize { get; set; }

        public int PageCount { get; set; }

        public int? Page { get; set; }
    }
}
