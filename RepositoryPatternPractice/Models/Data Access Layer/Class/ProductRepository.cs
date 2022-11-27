using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using RepositoryPatternPractice.Models.Business_Objet;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Policy;
using static Humanizer.In;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration Configuration;//connection Interface

      //  Product_Table product = new Product_Table();

        List<String> PriceHistoryList = new List<String>();
        List<Product_Table> productlist;
        // List<String> PriceHistoryList2 = new List<String> { "2000", "4000"};
        // List<int> PriceHistroyIDTrack = new List<int>();
        // List<Tuple<string, int>> PriceHistorylist = new List<Tuple<string, int>>();

        int Checkid = 0;

        public ProductRepository(IConfiguration config)
        {
            Configuration=config;
        }


        public void InsertProduct(Product_Table product_)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());
                
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Product_Table] VALUES(@productName,@productPrice," +

                                                                        "@productCompany,@productCount)", connection);

                cmd.Parameters.AddWithValue("@productName", product_.productName);
                cmd.Parameters.AddWithValue("@productCompany", product_.productCompany);
                cmd.Parameters.AddWithValue("@productPrice", product_.productPrice);
                cmd.Parameters.AddWithValue("@productCount", product_.productCount);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }



        public Product_Table GetProductById(int ProductId)
        {

            Product_Table product = new Product_Table();

            try
            {
                SqlConnection connection = new SqlConnection(Configuration
                    .GetConnectionString("DefaultConnection").ToString());

                connection.Open();

                string loadInforamtion = "SELECT * FROM Product_Table WHERE id=" + ProductId;
                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        product.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                        product.productName = Convert.ToString(dt.Rows[i]["productName"]);
                        product.productPrice = Convert.ToDouble(dt.Rows[i]["productPrice"]);
                        product.productCompany = Convert.ToString(dt.Rows[i]["productCompany"]);
                        product.productCount = Convert.ToInt32(dt.Rows[i]["productCount"]);

                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return product;


        }



        public List <Product_Table> GetProducts()
        {
            productlist = new List<Product_Table>();
           
            try
            {

                SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());

                connection.Open();
                string loadInforamtion = "SELECT * FROM Product_Table";
                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        Product_Table product = new Product_Table();

                        product.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                        product.productName = Convert.ToString(dt.Rows[i]["productName"]);
                        product.productPrice = Convert.ToDouble(dt.Rows[i]["productPrice"]);
                        product.productCompany = Convert.ToString(dt.Rows[i]["productCompany"]);
                        product.productCount = Convert.ToInt32(dt.Rows[i]["productCount"]);
                       //product.PriceHistory = PriceHistoryList2;
                        if (product.Id == Checkid)
                        {
                            product.PriceHistory = PriceHistoryList;
                        }

                        productlist.Add(product);
                        

                    }
                    
                }
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return productlist;

        }

        public void UpdateProduct(Product_Table product_)
        {

            //throw new NotImplementedException();

            try
            {
                SqlConnection connection = new SqlConnection(Configuration
               .GetConnectionString("DefaultConnection").ToString());
                connection.Open();


                SqlCommand cmd = new SqlCommand("UPDATE Product_Table SET productName = @productName," +
                    " productCompany = @productCompany , productPrice=@productPrice , productCount=@productCount " +
                    "WHERE id ='" + product_.Id + "'", connection); 

                cmd.Parameters.AddWithValue("@productName", product_.productName);
                cmd.Parameters.AddWithValue("@productCompany", product_.productCompany);
                cmd.Parameters.AddWithValue("@productPrice", product_.productPrice);
                cmd.Parameters.AddWithValue("@productCount", product_.productCount);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        [HttpPost]
        public void DeleteProduct(int ProductId)
        {
          try
           {

            SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection").ToString());

            connection.Open();

            string loadInforamtion = "DELETE FROM Product_Table WHERE id='" + ProductId + "'";

            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            comm.ExecuteNonQuery();

                connection.Close();
            }

          catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }

        }



        public List<Product_Table> GetProductHistorybyId(int ProductId)
        {

            Product_Table product = new Product_Table();

            //List<ProductHistory> productHistorylist = new List<ProductHistory>();

            try
            {
                SqlConnection connection = new SqlConnection(Configuration
                    .GetConnectionString("DefaultConnection").ToString());

                connection.Open();

                string loadInforamtion = "SELECT PriceHistory, ProductId From Product_Table right join ProductHistory ON ProductHistory.ProductId = Product_Table.id Where Product_Table.id = " + ProductId;
                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    Checkid = ProductId;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ProductHistory productHistory = new ProductHistory();

                       
                        productHistory.PriceHistory = Convert.ToString(dt.Rows[i]["PriceHistory"]);
                        //product.Id = Convert.ToInt32(dt.Rows[i]["ProductId"]);

                        //product.PriceHistory.Add(productHistory.PriceHistory);
                        PriceHistoryList.Add(productHistory.PriceHistory);
                       // PriceHistroyIDTrack.Add(product.Id);
                        
                      //  PriceHistorylist.Add(Tuple.Create(productHistory.PriceHistory, product.Id));



                    }
                }

                //if (productlist.Count > 0)
                //{
                //    for (int i = 0; i < productlist.Count; i++)
                //    {
                //        if (productlist[i].Id == Checkid)
                //        {
                //            productlist[i].PriceHistory = PriceHistoryList;
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return productlist;

        }



    public List<string> GetFormField()
    {

            SqlConnection connection = new SqlConnection(Configuration
                   .GetConnectionString("DefaultConnection").ToString());

            connection.Open();

            string loadInforamtion = "SELECT * FROM Product_Table";
            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                List<string> FieldList = dt.Columns
                                           .Cast<DataColumn>()
                                           .Select(c => c.ColumnName)
                                           .ToList();
                for(int i=0; i<FieldList.Count(); i++)
                {
                   Debug.WriteLine(FieldList[i]);
                }
                return FieldList;
            }

            return null;
            
        }





    }
}
