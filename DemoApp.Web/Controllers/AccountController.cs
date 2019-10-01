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


        public async Task<IActionResult> Register()
        {
            try
            {
                ViewBag.Message = "User already registered.";

                AppUser user = await UserManager.FindByNameAsync("TestUser");
                if (user == null)
                {
                    user = new AppUser()
                    {
                        UserName = "TestUser",
                        Email = "testUser@test.com",
                        FirstName = "John",
                        LastName = "Doe"
                    };

                    IdentityResult result = await UserManager.CreateAsync(user, "Test123!");
                    ViewBag.Message = "User was created!";
                }

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return View();
        }

        public async Task<IActionResult> Login()
        {
            var result = await SignInManager.PasswordSignInAsync("TestUser", "Test123!", false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "result is: " + result.ToString();
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