using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            appUser.Contragents.Add(newContragent);

            await Context.SaveChangesAsync();

            return View(model: appUser);

        }

        public async Task<IActionResult> ShowAll()
        {
            var appUser = await UserManager.GetUserAsync(HttpContext.User);
            ICollection<Contragent> contCollection = new List<Contragent>();
            

            foreach (var cont in Context.Contragents)
            {
                if (cont.AppUser.Id == appUser.Id)
                {
                    contCollection.Add(cont);    
                }    
            }

            return View(contCollection);
        }

        public IActionResult Details(int Id)
        {
            var cont = Context.Contragents.Find(Id);

            return View(model: cont);
        }
    }
}