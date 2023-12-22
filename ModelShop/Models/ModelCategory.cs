namespace ModelShop.Models
{
    public class ModelCategory
    {
        public int ModelCategoryID { get; set; }

        public string Name { get; set; }

        // reference navigation
        public virtual ICollection<Model3D> Models { get; set; }
    }
}
