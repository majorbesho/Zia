﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArName { get; set; }
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        [Display(Name = "Short Name")]
        public string ArShortName { get; set; }

        [Display(Name = "Barcode")]
        public string Barcode { get; set; }
        public string img { get; set; }
        public string type { get; set; }
        [Required]
        [Range(typeof(double), "0", "79228162514264337593543950335")]
        
        public double publicPrice { get; set; }
        [Range(typeof(double), "0", "79228162514264337593543950335")]
        public double priceAfterDisCont { get; set; }
        public bool isActive { get; set; }
        
        public string shortDis { get; set; }

     
        [Display(Name = "size")]
        public string size { get; set; }

        public enum Esize { kilo = 1, Half = 2, quarter = 3, Cartoon = 4, cat5 = 5, }

        

        public enum EDis { cat=1, cat2=2, cat3 = 3, cat4 = 4, cat5 = 5, }

        public string LongDis { get; set; }
        public string Specifications { get; set; }
        public string ArLongDis { get; set; }
        public string ArSpecifications { get; set; }
        public int CategoryId  { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }



    }
}
