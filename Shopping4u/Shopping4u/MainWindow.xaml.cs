using Shopping4u.Commands;
using Shopping4u.ViewModels;
using Shopping4u.Models;
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
using Shopping4u.Views;

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl currentPage;

        private HomePage homePage = new HomePage();
        private ShoppingListPage recommendedShoppingListPage = new ShoppingListPage(new RecommendedShoppingListViewModel(new ReccomendedShoppingListModel()));
        private ShoppingListPage myShoppingListPage = new ShoppingListPage(new MyShoppingListViewModel(new MyShoppingListModel()));
        private ShoppingHistoryPage shoppingHistoryPage = new ShoppingHistoryPage();
        private StatisticsPage statisticsPage = new StatisticsPage();

        public MainWindow()
        {
            InitializeComponent();
            goToPage(homePage);
            
            addRecommendtion();
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
            myShoppingListPage.DataContext = new MyShoppingListViewModel(new MyShoppingListModel());
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

        private void addRecommendtion()
        {
            Recommendtion.Children.Add(new RecommendtionUserControl());
        }

    }
}
