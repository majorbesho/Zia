using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            coutnt = 1;
        }
        [Key] 
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int ItemId { get; set; }
        [NotMapped]
        [ForeignKey("ItemId")]
        public virtual Item Items { get; set; }
        [Range(1,int.MaxValue)]
        public int coutnt { get; set; }



    }
}
