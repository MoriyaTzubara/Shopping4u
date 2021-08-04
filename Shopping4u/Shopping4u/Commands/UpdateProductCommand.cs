using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.Commands
{
    public class UpdateProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShoppingListViewModel shoppingListViewModel;

        public UpdateProductCommand(ShoppingListViewModel shoppingListViewModel)
        {
            this.shoppingListViewModel = shoppingListViewModel;
        }

        public bool CanExecute(object parameter)
        {
            // SHOULD BE IMPLEMENTED
            return true;
        }

        public void Execute(object parameter)
        {
            // SHOULD BE IMPLEMENTED
            shoppingListViewModel.UpdateProduct(parameter as OrderedProduct);
        }
    }
}
