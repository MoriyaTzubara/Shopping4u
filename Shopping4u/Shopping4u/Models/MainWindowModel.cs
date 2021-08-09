using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace Shopping4u.Models
{
    public class MainWindowModel
    {
        public int CountProductsInMyShoppingList { get; set; }

        public IEnumerable<OrderedProduct> recommendedProducts { get; set; }


    }
}
