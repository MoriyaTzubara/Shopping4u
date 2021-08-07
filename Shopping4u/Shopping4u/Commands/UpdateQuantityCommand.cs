using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shopping4u.Extensions;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class UpdateQuantityCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IProductViewModel iProductViewModel;

        public UpdateQuantityCommand(IProductViewModel iProductViewModel)
        {
            this.iProductViewModel = iProductViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int num = 0;
            Int32.TryParse(parameter.ToString(), out num);

            iProductViewModel.Quantity += num;
        }
    }
}
