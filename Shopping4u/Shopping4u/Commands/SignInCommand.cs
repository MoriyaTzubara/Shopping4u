using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shopping4u.Commands
{
    public class SignInCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private SignInViewModel signInViewModel;

        public SignInCommand(SignInViewModel signInViewModel)
        {
            this.signInViewModel = signInViewModel;
        }

        public bool CanExecute(object parameter)
        {

            return true;
        }

        public void Execute(object parameter)
        {
            SignInViewModel signInVM = parameter as SignInViewModel;

            bool result = true;
            if (string.IsNullOrEmpty(signInVM.Email))
                result = false;

            if (string.IsNullOrEmpty(signInVM.Password))
                result = false;

            signInViewModel.SetCanSignIn(result);

            if (!result)
                return;

            signInViewModel.SignIn(signInVM.Email, signInVM.Password);
        }
    }
}
