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



        //Get Signup
        public IActionResult Signup(Users user)
        {
            //if (_UsersRepository.CreateUser(user)) {

            //   return RedirectToAction("Index", "Home");
            // }
            //  return RedirectToAction("Index","Home");
            if (user.username== null)
            {
                return View(new Users());
            }
            _UsersRepository.CreateUser(user);
            return RedirectToAction("Index", "Home");

        }


       
        public IActionResult Login(PersonLogin pl)
        {
            if(pl.email==null || pl.password == null)
            {
                return View(new PersonLogin());
            } 
            
            _UsersRepository.login(pl);
            return RedirectToAction("Index", "Home");

        }
    }
}
