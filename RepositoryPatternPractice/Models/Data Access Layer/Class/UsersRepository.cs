using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using RepositoryPatternPractice.Models.Business_Objet;
using RepositoryPatternPractice.Models.Data_Access_Layer.Interface;
using System.Configuration;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class UsersRepository: Interface.IUsersRepository
    {


        private readonly IConfiguration Configuration;//connection Interface
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository (IConfiguration config)
        {
            Configuration = config;//need it for Configuration in line 25
          //  _roleManager= roleManager;

        }


        public void CreateUser(Users user) {

            try
            {

                SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection").ToString());

                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Product_Table] VALUES(@UserName,@," +

                                "@UserEmail,@UserPassword,@UserRole)", connection);

                Users users = new Users();

              //  cmd.Parameters.AddWithValue("@productName", user.);
              //  cmd.Parameters.AddWithValue("@productCompany", product_.productCompany);
              //  cmd.Parameters.AddWithValue("@productPrice", product_.productPrice);
              //  cmd.Parameters.AddWithValue("@productCount", product_.productCount);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        public void ProvideRole(Users user) { }

        public void checkExistingUser(Users user) { }

        public void updateUser(Users user) { }


    }
}
