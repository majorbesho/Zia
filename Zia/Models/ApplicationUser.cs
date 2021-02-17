using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zia.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string city { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Tele { get; set; }

    }
}
