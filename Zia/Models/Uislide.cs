using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Uislide
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    
        public string Img { get; set; }
      
        public bool IsActive { get; set; }


    }
}
