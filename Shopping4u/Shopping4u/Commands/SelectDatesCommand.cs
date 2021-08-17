using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class SelectDatesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ILineChartViewModel lineChartViewModel;

        public SelectDatesCommand(ILineChartViewModel lineChartViewModel)
        {
            this.lineChartViewModel = lineChartViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DateTime start = (parameter as ILineChartViewModel).StartDate;
            DateTime end = (parameter as ILineChartViewModel).EndDate;
            
            lineChartViewModel.selectDates(start, end);
        }
    }
}
