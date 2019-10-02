using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Web.Controllers
{
    public class ContragentController : Controller
    {
        private IdentityAppContext Context { get; }

        private UserManager<AppUser> UserManager { get; }


        public ContragentController(IdentityAppContext context, UserManager<AppUser> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Addcontragent()
        {
            var appUser = await UserManager.GetUserAsync(HttpContext.User);

            return View(appUser);
        }


        [HttpPost]
        public async Task<IActionResult> AddContragent(string name, string address, string email, string VATNumber)
        {
            var appUser = await UserManager.GetUserAsync(HttpContext.User);

            var newContragent = new Contragent()
            {
                Name = name,
                Address = address,
                Email = email,
                VATNumber = VATNumber,
                AppUser = appUser
            };

            await Context.Contragents.AddAsync(newContragent);

            Context.AppUsers.FirstOrDefault(x => x.Id == appUser.Id).Contragents.Add(newContragent);


            await Context.SaveChangesAsync();

            return View(model: appUser);

        }
    }
}