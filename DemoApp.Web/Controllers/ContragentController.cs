using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

        public async Task<IActionResult> AddContragent(string name, string address, string email, string VATNumber)
        {
            try
            {
                ViewBag.Message = "";

                //var entry = Context.Contragents.FirstOrDefault(con => con.VATNumber == VATNumber);

                if (true)
                {
                    var userToBeAdded = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var addedContragent = new Contragent()
                    {
                        Name = name,
                        Address = address,
                        Email = email,
                        VATNumber = VATNumber,
                        AppUser = Context.AppUsers.FirstOrDefault(x=> x.Id == Int32.Parse(userToBeAdded))
                    };

                    ViewBag.Message = "Contragent added!";

                    Context.Contragents.Add(addedContragent);
                    await Context.SaveChangesAsync();
                }
                else
                {
                    
                }
            }
            catch (Exception e)
            {
                ViewBag.Messaage = e.Message;
            }

            return View();
        }
    }
}