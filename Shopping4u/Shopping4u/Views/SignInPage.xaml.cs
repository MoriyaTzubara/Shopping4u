using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shopping4u.Views
{
    /// <summary>
    /// Interaction logic for SignInUserControl.xaml
    /// </summary>
    public partial class SignInPage : UserControl, INotifyPropertyChanged
    {
        private SignInViewModel signInViewModel;
        public SignInPage(SignInViewModel _signInViewModel)
        {
            InitializeComponent();
            signInViewModel = _signInViewModel;
            DataContext = signInViewModel;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.flag.Tag = true;
        }
    }
}
