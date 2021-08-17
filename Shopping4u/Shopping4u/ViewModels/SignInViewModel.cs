using BE;
using Shopping4u.Commands;
using Shopping4u.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public event EventHandler<Consumer> SignInSuccessEvent;

        public SignInCommand SignInCommand { get; set; }


        public void SetCanSignIn(bool result)
        {
            ErrorMessage = result ? "" : "Please fill both email and password";
        }

        private bool canSignIn;

        public bool CanSignIn
        {
            get { return canSignIn; }
            set { canSignIn = value; OnPropertyChanged(); }
        }
   

        private string errorMessage;
        
        public string ErrorMessage 
        { 
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }



        public SignInViewModel()
        {
            signInModel = new SignInModel();
            SignInCommand = new SignInCommand(this);
            CanSignIn = true;
        }

        public bool SignIn(string email, string password)
        {
            MessageBox.Show($"Sign in: email = {email}, password = {password}");
            
            if (signInModel.SignIn(email, password))
            {
                App.Consumer = signInModel.GetConsumer(email);
                SignInSuccessEvent(this, App.Consumer);
                return true;
            }

            ErrorMessage = "Invalid username or password";
            return false;
        }
    }
}
