using BE;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class SignInPage : UserControl
    {
        public SignInPage(SignInViewModel signInViewModel)
        {
            InitializeComponent();
            DataContext = this;
            signInViewModel.SignInSuccess += closeWindow;
        }

        private void closeWindow(object sender, Consumer e)
        {
            
        }
    }
}
