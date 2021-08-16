using BE;
using Shopping4u.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class SignInViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private SignInModel signInModel;

        public event EventHandler<Consumer> SignInSuccess;

        private string errorMessage;
        public string ErrorMessage 
        { 
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }

        public SignInViewModel()
        {
            signInModel = new SignInModel();
        }

        public bool SignIn(string userName, string password)
        {
            if (signInModel.SignIn(userName, password))
            {
                App.Consumer = signInModel.GetConsumer(userName);
                SignInSuccess(this, App.Consumer);
                return true;
            }

            ErrorMessage = "Invalid username or password";
            return false;
        }
    }
}
