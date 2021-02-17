using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public enum etype {percent=0, total=1 }

        [Required]
        public bool IsActive { get; set; }
        public double MinimumAmount {get; set; }
        public double Dicount { get; set; }
    }
}
