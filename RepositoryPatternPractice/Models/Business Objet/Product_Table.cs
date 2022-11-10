namespace RepositoryPatternPractice.Models
{
    public class Product_Table
    {

        public int Id { get; set; }
        public string productName { get; set; }

        public string productCompany { get; set; }

        public double productPrice { get; set; }

        public int productCount { get; set; }

       // public List <Product_Table> ProductList { get; set; }
    }
}
