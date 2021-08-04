using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
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

        public int ShoppingListId {get; set;}
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int BranchProductId { get; set; }

        public ProductViewModel(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;

            ShoppingListId = orderedProduct.shoppingListId;
            UnitPrice = orderedProduct.unitPrice;
            Quantity = orderedProduct.quantity;
            BranchProductId = orderedProduct.branchProductId;
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

    }
}
