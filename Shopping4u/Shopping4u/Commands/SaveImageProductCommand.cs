using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopping4u.ViewModels;

namespace Shopping4u.Commands
{
    public class SaveImageProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private CreateProductViewModel createProductViewModel;

        public SaveImageProductCommand(CreateProductViewModel createProductViewModel)
        {
            this.createProductViewModel = createProductViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string imgUrl = (parameter as CreateProductViewModel).ImgUrl;
            (parameter as CreateProductViewModel).ImgUrl = "";
            createProductViewModel.ImgUrl = "";
            int branchProductId = (parameter as CreateProductViewModel).BranchProductId;
            createProductViewModel.SaveImageProduct(imgUrl, branchProductId);
        }
    }
}
