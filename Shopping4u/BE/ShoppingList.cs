using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ShoppingList
    {
        public int id;
        public int consumerId;
        public List<OrderedProduct> products;
        public DateTime date;
        public bool approved;
    }
}
