using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using RepositoryPatternPractice.Models.Business_Objet;
using RepositoryPatternPractice.Models.Data_Access_Layer.Interface;
using System.Configuration;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Humanizer.In;

namespace RepositoryPatternPractice.Models.Data_Access_Layer.Class
{
    public class UsersRepository: Interface.IUsersRepository
    {


        private readonly IConfiguration Configuration;//connection Interface
        //private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository (IConfiguration config)
        {
            Configuration = config;//need it for Configuration in line 25
          //  _roleManager= roleManager;

        }


        public void CreateUser(Users user) {

            try
            {
                //Inser User Info

                SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection").ToString());

                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [User_Table] VALUES(@UserName," +

                "@UserEmail,@UserPassword)", connection);



                cmd.Parameters.AddWithValue("@UserName", user.username);
                cmd.Parameters.AddWithValue("@UserEmail", user.userEmail);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);

                cmd.ExecuteNonQuery();


                //Fetching Data

                string loadInforamtion = "SELECT id FROM User_Table WHERE useremail='" + user.userEmail + "'";

                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                Users users = new Users();

                if (dt.Rows.Count > 0)
                {
                  users.id = Convert.ToInt32(dt.Rows[0]["id"]);
                }

                //Fetch Role ID from Role Table

                int RoleID = (int)Convert.ToInt64(user.userRole);

                string loadRoleID = "SELECT RoleID FROM RoleTable WHERE RoleID=" + RoleID ;
                comm = new SqlCommand(loadRoleID, connection);
                sqlDataAdapter = new SqlDataAdapter(comm);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                    

                if (dt.Rows.Count > 0)
                {
                RoleID = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                }

                //Create Role User Mapper



                cmd = new SqlCommand("INSERT INTO [UserMapRole_Table] VALUES(@UserID," +

                "@RoleID)", connection);


                cmd.Parameters.AddWithValue("@UserID",users.id);
                cmd.Parameters.AddWithValue("@RoleID", RoleID);
            //    cmd.Parameters.AddWithValue("@UserPassword", user.Password);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }




        public String loginAsync(PersonLogin pl)
        {


            SqlConnection connection = new SqlConnection(Configuration
            .GetConnectionString("DefaultConnection").ToString());

            connection.Open();
            
            
         //Give Role For User Email
          string loadInforamtion = "SELECT RoleName FROM RoleTable WHERE RoleID IN (SELECT RoleID FROM UserMapRole_Table " +
          "INNER JOIN User_Table ON (SELECT id FROM User_Table WHERE useremail='" + pl.email + "')=UserMapRole_Table.UserID)";


            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            String RoleName="NoRole";

            if (dt.Rows.Count > 0)
            {
                RoleName = Convert.ToString(dt.Rows[0]["RoleName"]);
            }

            return RoleName; 
        
        }


        public void SignOut()
        {

        }


        public void ProvideRole(Users user) { }

        public void checkExistingUser(Users user) { }

        public void updateUser(Users user) { }


    }
}
