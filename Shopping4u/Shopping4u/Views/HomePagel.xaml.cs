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
using Shopping4u.Commands;
using Shopping4u.ViewModels;

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl, INotifyPropertyChanged
    {
        private string consumerName;
        public string ConsumerName { get { return consumerName; } set { consumerName = value; OnPropertyChanged(); } }
        public GoToRecommendedShoppingListPageCommand GoToRecommendedShoppingListPageCommand { get; set; }

        public string Greeting
        {
            get
            {
                if (DateTime.Now.Hour < 13 && DateTime.Now.Hour >= 6)
                    return "Good Morning,";
                else if (DateTime.Now.Hour >= 13 && DateTime.Now.Hour <= 17)
                    return "Good Afternoon,";
                else
                    return "Good Night,";
            }
        }

        public HomePage()
        {
            InitializeComponent();
            GoToRecommendedShoppingListPageCommand = new GoToRecommendedShoppingListPageCommand();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
