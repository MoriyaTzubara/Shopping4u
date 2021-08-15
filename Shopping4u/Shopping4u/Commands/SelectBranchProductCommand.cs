using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.Commands
{
    public class SelectBranchProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private CreateProductViewModel createProductViewModel;

        public SelectBranchProductCommand(CreateProductViewModel createProductViewModel)
        {
            this.createProductViewModel = createProductViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                return;
            BranchProduct branchProduct = ((BranchProductViewModel) parameter).branchProduct;
            if(branchProduct.branchProductId == 0)
                System.Windows.MessageBox.Show("This product is not avaliable in this branch");
            createProductViewModel.BranchProductSelected(branchProduct);
        }
    }
}
