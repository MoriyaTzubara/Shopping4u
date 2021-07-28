using Shopping4u.Commands;
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

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl currentPage;

        private HomePage homePage = new HomePage();
        private RecommendedShoppingListPage recommendedShoppingListPage = new RecommendedShoppingListPage();
        private MyShoppingListPage myShoppingListPage = new MyShoppingListPage();
        private ShoppingHistoryPage shoppingHistoryPage = new ShoppingHistoryPage();
        private StatisticsPage statisticsPage = new StatisticsPage();

        public MainWindow()
        {
            InitializeComponent();
            goToPage(homePage);
            
            DataContext = new MainWindowViewModel(this);
        }

        public void GoToHomePage()
        {
            goToPage(homePage);
        }

        public void GoToRecommendedShoppingListPage()
        {
            goToPage(recommendedShoppingListPage);
        }

        public void GoToMyShoppingListPage()
        {
            goToPage(myShoppingListPage);
        }

        public void GoToShoppingHistoryPage()
        {
            goToPage(shoppingHistoryPage);
        }

        public void GoToStatisticsPage()
        {
            goToPage(statisticsPage);
        }

        private void goToPage(UserControl page)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(page);
            currentPage = page;
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
