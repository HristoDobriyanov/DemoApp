﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Web.Models
{
    public class Contragent
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string VATNumber { get; set; }

        public AppUser AppUser { get; set; }
    }
}
