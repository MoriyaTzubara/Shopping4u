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
    public class SaveShoppingListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MyShoppingListViewModel myShoppingListViewModel;

        public SaveShoppingListCommand(MyShoppingListViewModel myShoppingListViewModel)
        {
            this.myShoppingListViewModel = myShoppingListViewModel;
        }

        public bool CanExecute(object parameter)
        {
            // SHOULD BE IMPLEMENTED
            return true;
        }

        public void Execute(object parameter)
        {
            // SHOULD BE IMPLEMENTED
            myShoppingListViewModel.SaveShoppingList();
        }
    }
}
