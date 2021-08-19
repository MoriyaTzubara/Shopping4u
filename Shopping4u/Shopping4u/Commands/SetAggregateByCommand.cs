using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class SetAggregateByCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ChartsPageViewModel chartsPageViewModel;

        public SetAggregateByCommand(ChartsPageViewModel _chartsPageViewModel)
        {
            chartsPageViewModel = _chartsPageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AggregateBy aggregateBy = AggregateBy.WEEK;
            switch (parameter.ToString().ToLower())
            {
                case "month":
                    aggregateBy = AggregateBy.MONTH;
                    break;
                case "day":
                    aggregateBy = AggregateBy.DAY;
                    break;
                case "week":
                    aggregateBy = AggregateBy.WEEK;
                    break;
                default:
                    break;
            }
            chartsPageViewModel.SetAggregateBy(aggregateBy);  
        }
    }
}
