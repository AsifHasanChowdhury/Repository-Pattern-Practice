using RepositoryPatternPractice.Models.Business_Objet;

namespace RepositoryPatternPractice.Models
{
    public class Product_Table
    {

        public int Id { get; set; }
        public string productName { get; set; }

        public string productCompany { get; set; }

        public double productPrice { get; set; }

        public int productCount { get; set; }

        public List <String> PriceHistory=new List<String>();

        public List<int> PriceHistroyID = new List<int>();
       //public List <Tuple<string, int>> PriceHistory = new List <Tuple<string,int>> ();

    }
}
