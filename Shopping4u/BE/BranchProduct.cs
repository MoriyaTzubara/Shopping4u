using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BranchProduct
    {
        public int productId;
        public int branchId;
        public double price;
        public int branchProductId;

        public override string ToString()
        {
            return $"productId: {productId}, branchId: {branchId}, price: {price}, branchProductId: {branchProductId}";
        }
    }
}
