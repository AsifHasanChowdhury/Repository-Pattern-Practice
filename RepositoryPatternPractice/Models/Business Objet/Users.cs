using MessagePack;
using Microsoft.Build.Framework;

namespace RepositoryPatternPractice.Models.Business_Objet
{
    public class Users
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string userEmail { get; set; }

        [Required]        
        public string password { get; set; }

        [Required]
        public string userRole { get; set; }




       
    }
}
