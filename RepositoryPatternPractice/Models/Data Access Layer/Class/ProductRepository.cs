﻿using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Security.Policy;
using static Humanizer.In;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration Configuration;//connection Interface



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

                SqlCommand cmd = new SqlCommand("INSERT INTO [Product_Table] VALUES(@productName,@productCompany," +

                                                                        "@productPrice,@productCount)", connection);

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
                        product.productPrice = Convert.ToInt32(dt.Rows[i]["productPrice"]);
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
            List<Product_Table> productlist = new List<Product_Table>();

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
                        product.productPrice = Convert.ToInt32(dt.Rows[i]["productPrice"]);
                        product.productCompany = Convert.ToString(dt.Rows[i]["productCompany"]);
                        product.productCount = Convert.ToInt32(dt.Rows[i]["productCount"]);

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

    }
}
