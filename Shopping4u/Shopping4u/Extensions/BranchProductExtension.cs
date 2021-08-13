using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Extensions
{
    public static class BranchProductExtension
    {
        public static Branch GetBranch(this BranchProduct branchProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetBranch(bl.GetBranchProduct(branchProduct.branchProductId).branchId);
        }
        public static Product GetProduct(this BranchProduct branchProduct)
        {
            IBL bl = new BL.BL();
            return bl.GetProduct(bl.GetBranchProduct(branchProduct.productId).productId);
        }
    }
}
