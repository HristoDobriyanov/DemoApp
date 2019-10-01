using System;
using System.Threading.Tasks;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> SignInManager { get; }

        private UserManager<AppUser> UserManager { get; }


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        
        public async Task<IActionResult> Register(string userName, string password, string email, string firstName, string lastName)
        {
            try
            {
                ViewBag.Message = "User already registered.";

                AppUser user = await UserManager.FindByNameAsync(userName);

                if (user == null)
                {
                    user = new AppUser()
                    {
                        UserName = userName,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName
                    };

                    IdentityResult result = await UserManager.CreateAsync(user, password);
                    ViewBag.Message = "User was created!";
                        
                    
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(userName, password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "The result is: " + result.ToString();
            }

            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}