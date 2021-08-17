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
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            MainWindowViewModel = new MainWindowViewModel(this);
            DataContext = MainWindowViewModel;

            if (App.Consumer != null)
                GoToHomePage();
            else
                GoToSignInPage();
            MainWindowViewModel.AddedRecommendtionEvent += addRecommendtion;
        }

        public void GoToHomePage()
        {
            goToPage(MainWindowViewModel.homePage);
        }

        public void GoToRecommendedShoppingListPage()
        {
            goToPage(MainWindowViewModel.recommendedShoppingListPage);
        }

        public void GoToMyShoppingListPage()
        {
            MainWindowViewModel.myShoppingListPage.DataContext = new MyShoppingListViewModel(new MyShoppingListModel());
            goToPage(MainWindowViewModel.myShoppingListPage);
        }

        public void GoToShoppingHistoryPage()
        {
            goToPage(MainWindowViewModel.shoppingHistoryPage);
        }

        public void GoToStatisticsPage()
        {
            goToPage(MainWindowViewModel.chartsPage);
        }

        public void GoToSignInPage()
        {
            goToPage(MainWindowViewModel.signInPage);
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

        private void addRecommendtion(object sender, RecommendtionViewModel recommendtionViewModel)
        {
            this.Dispatcher.Invoke(() =>
            {
                Recommendtion.Children.Add(new RecommendtionUserControl(recommendtionViewModel));
            });
        }


    }
}
