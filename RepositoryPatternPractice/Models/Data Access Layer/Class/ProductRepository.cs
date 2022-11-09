using Humanizer;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Security.Policy;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration Configuration;



        public ProductRepository(IConfiguration config)
        {
            Configuration=config;
        }


        public void InsertProduct(Product_Table product_)
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());


            SqlCommand cmd = new SqlCommand("INSERT INTO [Product_Table] VALUES(@productName,@productCompany," +

                                                                    "@productPrice,productCount)", connection);

            cmd.Parameters.AddWithValue("@productName", product_.productName);
            cmd.Parameters.AddWithValue("@productCompany", product_.productCompany);
            cmd.Parameters.AddWithValue("@productPrice", product_.productPrice);
            cmd.Parameters.AddWithValue("@productCount", product_.productCount);

            cmd.ExecuteNonQuery();
        }


        public Product_Table GetProductById(int ProductId)
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());

            connection.Open();

            string loadInforamtion = "SELECT * FROM Product_Table";
            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            Product_Table product = new Product_Table();
            

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    product.productName = Convert.ToString(dt.Rows[i]["productName"]);
                    product.productPrice = Convert.ToInt32(dt.Rows[i]["productPrice"]);
                    product.productCompany = Convert.ToString(dt.Rows[i]["productCompany"]);
                    product.productCount = Convert.ToInt32(dt.Rows[i]["productCount"]);

                }

            }

            return product;


        }


        public List<Product_Table> GetProducts()
        {
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());

            connection.Open();

            string loadInforamtion = "SELECT * FROM Product_Table";
            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            List<Product_Table> productlist = new List<Product_Table>();

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    
                    Product_Table product = new Product_Table();

                    product.productName = Convert.ToString(dt.Rows[i]["productName"]);
                    product.productPrice = Convert.ToInt32(dt.Rows[i]["productPrice"]);
                    product.productCompany= Convert.ToString(dt.Rows[i]["productCompany"]);
                    product.productCount = Convert.ToInt32(dt.Rows[i]["productCount"]);

                    productlist.Add(product);

                }

            }

            return productlist;


        }


        public void UpdateProduct(Product_Table product_)
        {
            throw new NotImplementedException();
        }


        public void DeleteProduct(int ProductId)
        {
            throw new NotImplementedException();
        }

    }
}
