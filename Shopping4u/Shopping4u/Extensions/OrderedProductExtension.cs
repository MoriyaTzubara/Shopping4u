using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Extensions
{
    public static class OrderedProductExtension
    {
        public static Branch GetBranch(this OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetBranch(bl.GetBranchProduct(orderedProduct.branchProductId).branchId);
        }
        public static Product GetProduct(this OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetProduct(bl.GetBranchProduct(orderedProduct.branchProductId).productId);
        }
        public static BranchProduct GetBranchProduct(this OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetBranchProduct(orderedProduct.branchProductId);
        }
        public static ShoppingList GetShoppingList(this OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetShoppingList(orderedProduct.shoppingListId);
        }
    }
}
