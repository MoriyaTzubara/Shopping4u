using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class CreateProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShoppingListViewModel shoppingListViewModel;

        public CreateProductCommand(ShoppingListViewModel shoppingListViewModel)
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
            shoppingListViewModel.CreateProduct(parameter as OrderedProduct);
        }
    }
}
