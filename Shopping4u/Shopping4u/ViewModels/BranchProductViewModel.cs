using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Extensions;
using Shopping4u.BL;

namespace Shopping4u.ViewModels
{
    public class BranchProductViewModel: BranchProduct
    {
        string BranchName { get { return ((BranchProduct)this).GetBranch().name; } set { } }

        public BranchProductViewModel(BranchProduct branchProduct)
        {
            this.branchId = branchProduct.branchId;
            this.branchProductId = branchProduct.branchProductId;
            this.price = branchProduct.price;
            this.productId = branchProduct.productId;
        }

        public override string ToString()
        {
            return $"{BranchName} ({price}$)";
        }

    }
}
