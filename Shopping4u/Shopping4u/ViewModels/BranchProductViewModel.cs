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
        #region PROPERTIRES
        public BranchProduct branchProduct;
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        #endregion
        #region CONSTRUCTOR
        public BranchProductViewModel(BranchProduct branchProduct)
        {
            this.branchProduct = branchProduct;

            this.BranchName = branchProduct.GetBranch().name;
            this.ProductName = branchProduct.GetProduct().name;
        }
        #endregion
        #region FUNCTIONS
        public override string ToString()
        {
            return $"{BranchName} ({branchProduct.price}$)";
        }
        #endregion
    }
}
