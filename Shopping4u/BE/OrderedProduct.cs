using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class OrderedProduct
    {
        public int id;
        public int shoppingListId;
        public int branchProductId;
        public double unitPrice;
        public int quantity;

        public override string ToString()
        {
            return $"shoppingListId: {shoppingListId}, branchProductId: {branchProductId}, unitPrice: {unitPrice}, quantity: {quantity}";
        }
    }
}
