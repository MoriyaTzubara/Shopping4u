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
        public OrderedProduct orderedProduct { get; set; }

        public ProductViewModel(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;
        }

        public String BranchName { 
            get { 
                IBL bl = new BL.BL();
                return bl.GetBranchName(orderedProduct.branchProductId);
            }
            private set { } }

        public String ProductName {
            get {
                IBL bl = new BL.BL();
                return "";
                //return bl.GetProductName(Product.);
            }
            set { } }
        
        public int Quantity { get { return orderedProduct.quantity; } set { orderedProduct.quantity = value; } }
        
    }
}
