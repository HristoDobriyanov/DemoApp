using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Web.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            this.Contragents= new List<Contragent>();
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Contragent> Contragents { get; set; }
    }
}
