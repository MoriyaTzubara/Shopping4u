using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class GoToRecommendedShoppingListPageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GoToRecommendedShoppingListPageCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (App.Current.MainWindow as MainWindow).GoToRecommendedShoppingListPage();
        }
    }
}
