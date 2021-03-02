using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zia.Models.ViewModel
{
    public class IndexViewModel
    {
        //public Item Items { get; set; }
        public IEnumerable<Item> Item { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Family> Family { get; set; }
        public IEnumerable<Uislide>Uislides { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        

    }
}
