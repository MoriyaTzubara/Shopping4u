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
    public class BranchProductViewModel
    {
        public BranchProduct branchProduct;

        public BranchProductViewModel(BranchProduct branchProduct)
        {
            this.branchProduct = branchProduct;

            this.BranchName = branchProduct.GetBranch().name;
            this.ProductName = branchProduct.GetProduct().name;
        }

        public string BranchName { get; set; }
        public string ProductName { get; set; }

        public override string ToString()
        {
            return $"{BranchName} ({branchProduct.price}$)";
        }

    }
}
