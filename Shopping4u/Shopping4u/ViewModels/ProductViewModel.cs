using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class ProductViewModel
    {
        public OrderedProduct Product { get; set; }

        public ProductViewModel(OrderedProduct orderedProduct)
        {
            Product = orderedProduct;
        }

        public String BranchName { get; set; }

        public String ProductName { get; set; }
        
        public int Quantity { get; set; }
        
    }
}
