using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class OrderedProduct
    {
        public int shoppingListId;
        public int branchProductId;
        public double unitPrice;
        public int quantity;
        
        public OrderedProduct(OrderedProduct orderedProduct)
        {
            this.quantity = orderedProduct.quantity;
            this.branchProductId = orderedProduct.branchProductId;
            this.unitPrice = orderedProduct.unitPrice;
            this.shoppingListId = orderedProduct.shoppingListId;
        }
        public OrderedProduct(){}
    }
}
