using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class SetStartDateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ChartsPageViewModel chartsPageViewModel;

        public SetStartDateCommand(ChartsPageViewModel _chartsPageViewModel)
        {
            chartsPageViewModel = _chartsPageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            chartsPageViewModel.SetStartDate((DateTime) parameter);
        }
    }
}
