using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class Category
    
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Discreption { get; set; }
        [Display(Name = "Family")]
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public virtual Family Family{ get; set; }
    }
}
