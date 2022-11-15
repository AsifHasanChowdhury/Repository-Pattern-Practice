using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternPractice.Models.Business_Objet;
using RepositoryPatternPractice.Models.Data_Access_Layer;
using RepositoryPatternPractice.Models.Data_Access_Layer.Class;
using RepositoryPatternPractice.Models.Data_Access_Layer.Interface;

namespace RepositoryPatternPractice.Controllers
{
    public class UserController : Controller
    {

        private IUsersRepository _UsersRepository;
        
        public UserController(IConfiguration configuration)
        {
            this._UsersRepository=new UsersRepository(configuration);
        }



        //Create Users Page
        public IActionResult Index()
        {
            return View(new Users());
        }
    }
}
