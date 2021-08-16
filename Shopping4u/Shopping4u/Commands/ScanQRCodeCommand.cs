using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.Extensions;
using BE;
using System.Windows.Forms;

namespace Shopping4u.Commands
{
    public class ScanQRCodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private CreateProductViewModel createProductViewModel;

        public ScanQRCodeCommand(CreateProductViewModel createProductViewModel)
        {
            this.createProductViewModel = createProductViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            createProductViewModel.ScanQRCode();
        }
    }
}

