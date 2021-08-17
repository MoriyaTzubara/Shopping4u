using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class SelectOptionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ILineChartViewModel lineChartViewModel;

        public SelectOptionCommand(ILineChartViewModel lineChartViewModel)
        {
            this.lineChartViewModel = lineChartViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            lineChartViewModel.selectOption(parameter);
        }
    }
}
