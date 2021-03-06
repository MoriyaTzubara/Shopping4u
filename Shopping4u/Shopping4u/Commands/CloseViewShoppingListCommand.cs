using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class CloseViewShoppingListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShoppingHistoryViewModel shoppingHistoryViewModel;
        public CloseViewShoppingListCommand(ShoppingHistoryViewModel _shoppingHistoryViewModel)
        {
            shoppingHistoryViewModel = _shoppingHistoryViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            shoppingHistoryViewModel.CloseViewShoppingList();
        }
    }
}
