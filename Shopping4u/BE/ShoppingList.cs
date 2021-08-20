using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ShoppingList
    {
        public static string[] allMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public int id;
        public int consumerId;
        public List<OrderedProduct> products;
        public DateTime date;
        public bool approved;
    }
}
