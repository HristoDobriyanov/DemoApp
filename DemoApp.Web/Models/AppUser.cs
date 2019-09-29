using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Web.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Contragent> Contragents { get; set; }
    }
}
