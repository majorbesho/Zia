using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models.ViewModel
{
    public class OrderDetailsCartViewModel
    {
        public List<ShopingCart> ShopingCartsList { get; set; }

        public OrderHeader OrderHeader { get; set; }

    }
}
