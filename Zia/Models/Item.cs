using System;
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
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Barcode")]
        public string Barcode { get; set; }
        public string img { get; set; }
        public string type { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        
        public decimal publicPrice { get; set; }
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal priceAfterDisCont { get; set; }
        public string isActive { get; set; }
        [Display(Name = "show in")]
        public string Dis { get; set; }
        public enum EDis { cat=1, cat2=2, cat3 = 3, cat4 = 4, cat5 = 5, }

        public int CategoryId  { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
