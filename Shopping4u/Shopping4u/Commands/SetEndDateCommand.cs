using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class SetEndDateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ChartsPageViewModel chartsPageViewModel;

        public SetEndDateCommand(ChartsPageViewModel _chartsPageViewModel)
        {
            chartsPageViewModel = _chartsPageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            chartsPageViewModel.SetEndDate((DateTime)parameter);
        }
    }
}
