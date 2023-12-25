using ModelShop.Models;

namespace ModelShop.ViewModels
{
    public enum OrderBy
    {
        Date,
        DateDescending,
        Views,
        ViewsDescending,
        Price,
        PriceDescending
    }

    public enum Criteria 
    {
        Default,
        Owned,
    }


    public class IndexViewModel
    {
        public IEnumerable<Model3D> Models3D { get; set; }

        public IEnumerable<ModelCategory> ModelCategories { get; set; }

        public string Search { get; set; }

        public IEnumerable<ModelCategory> ModelCategoriesSearch { get; set; }

        public OrderBy OrderBy { get; set; } = OrderBy.Date;

        public decimal MinPrice { get; set; } = 0;

        public decimal MaxPrice { get; set; } = 100000;

        public bool IsFindResult { get; set; }

        //public int PageSize { get; set; }

        public int Page { get; set; } = 1;

        public int PageCount { get; set; }

        public int ResultsCount { get; set; }
        //public int Page { get; set }
    }
}
