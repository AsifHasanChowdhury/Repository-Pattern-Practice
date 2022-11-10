using Microsoft.Build.Framework;

namespace RepositoryPatternPractice.Models.Business_Objet
{
    public class Users
    {
        [Required]
        int id { get; set; }

        [Required]
        string username { get; set; }

        [Required]
        string userEmail { get; set; }

        [Required]        
        string password { get; set; }

        [Required]
        string userRole { get; set; }




       
    }
}
