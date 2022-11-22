using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternPractice.Models.Business_Objet;
using RepositoryPatternPractice.Models.Data_Access_Layer.Class;
using RepositoryPatternPractice.Models.Data_Access_Layer.Interface;
using System.Security.Claims;

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

            else if (_UsersRepository.loginAsync(pl)!="NoRole")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, pl.email),
                    new Claim("FullName", pl.password),
                    new Claim(ClaimTypes.Role,_UsersRepository.loginAsync(pl)),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(5000000),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };



                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);


                return RedirectToAction("Index", "Home");
            }

            return View(new PersonLogin());
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");

        }
    }
}
