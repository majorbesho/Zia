using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public ShopingCart ShopingCart { get; set; }
        public IEnumerable<Item> item { get; set; }
    }
}
