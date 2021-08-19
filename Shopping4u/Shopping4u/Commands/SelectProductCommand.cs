using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.Commands
{
    public class SelectProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private CreateProductViewModel createProductViewModel;

        public SelectProductCommand(CreateProductViewModel createProductViewModel)
        {
            this.createProductViewModel = createProductViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Product product = parameter as Product;            
            if (product != null)
                createProductViewModel.ProductSelected(product);
        }
    }
}
