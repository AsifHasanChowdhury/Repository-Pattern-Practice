using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RepositoryPatternPractice.Models.Business_Objet
{
    public class Users
    {
        [Required]
        public int? id { get; set; }

        [Required]
        public string? username { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? userEmail { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }




        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }



        [Required]
        public string? userRole { get; set; }





    }
}
