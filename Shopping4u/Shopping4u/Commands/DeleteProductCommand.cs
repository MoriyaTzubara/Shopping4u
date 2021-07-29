using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    class DeleteProductCommand : ICommand
    {
        private ShoppingListViewModel shoppingListViewModel;
        public event EventHandler CanExecuteChanged;

        public DeleteProductCommand(ShoppingListViewModel shoppingListViewModel)
        {
            this.shoppingListViewModel = shoppingListViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int productId = (int) parameter;
            shoppingListViewModel.DeleteProduct(productId);
        }
    }
}
