using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Shopping4u.Extensions;
using BE;
using Shopping4u.Views;

namespace Shopping4u.Commands
{
    public class IgnoreRecommendationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public IgnoreRecommendationCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            // SHOULD BE IMPLEMENTED
            return true;
        }

        public void Execute(object parameter)
        {
            var recommendtion = parameter as RecommendtionUserControl;
            recommendtion.Visibility = Visibility.Collapsed;
        }
    }
}
