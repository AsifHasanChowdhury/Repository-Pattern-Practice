using Microsoft.AspNetCore.Mvc;
using RepositoryPatternPractice.Models.Data_Access_Layer;

namespace RepositoryPatternPractice.Controllers
{
    public class UserController : Controller
    {

       // private IUsersRepository _UsersRepository;
        
        public UserController(IConfiguration configuration)
        {



        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
