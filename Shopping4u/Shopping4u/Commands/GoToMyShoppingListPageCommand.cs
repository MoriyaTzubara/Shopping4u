using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class GoToMyShoppingListPageCommand: ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainWindow window;

        public GoToMyShoppingListPageCommand(MainWindow mainWindow)
        {
            window = mainWindow;
        }

        public bool CanExecute(object parameter)
        {
            return App.Consumer != null;
        }

        public void Execute(object parameter)
        {
            window.GoToMyShoppingListPage();
        }

    }
}
