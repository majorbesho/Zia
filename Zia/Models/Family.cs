﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Family
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Discrption { get; set; }
        public string Nots { get; set; }

    }
}
