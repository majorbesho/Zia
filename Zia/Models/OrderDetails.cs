using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item  Item{ get; set; }

        public string Name { get; set; }
        public int count { get; set; }
        public string Discription { get; set; }

        public double Price { get; set; }



    }
}
