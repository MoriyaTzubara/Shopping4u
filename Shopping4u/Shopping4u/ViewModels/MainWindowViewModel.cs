using Shopping4u.Commands;
using Shopping4u.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Shopping4u.Views;

namespace Shopping4u.ViewModels
{
    public class MainWindowViewModel
    {
        private ReccomendedShoppingListModel reccomendedShoppingListModel;
        private RecommendedShoppingListViewModel recommendedShoppingListViewModel;
        private SignInViewModel signInViewModel;

        public HomePage homePage = new HomePage();
        public ShoppingListPage recommendedShoppingListPage;
        public ShoppingListPage myShoppingListPage = new ShoppingListPage(new MyShoppingListViewModel(new MyShoppingListModel()));
        public ShoppingHistoryPage shoppingHistoryPage = new ShoppingHistoryPage();
        public SignInPage signInPage;
        public ChartsPage chartsPage = new ChartsPage();


        public GoToRecommendedShoppingListPageCommand GoToRecommendedShoppingListPageCommand { get; set; }
        public GoToHomePageCommand GoToHomePageCommand { get; set; }
        public GoToMyShoppingListPageCommand GoToMyShoppingListPageCommand { get; set;}
        public GoToShoppingHistoryPageCommand GoToShoppingHistoryPageCommand { get; set; }
        public GoToStatisticsPageCommand GoToStatisticsPageCommand { get; set; }
        public GoToSignInPageCommand GoToSignInPageCommand { get; set; }

        public CreateProductCommand CreateProductCommand { get; set; }

        public event EventHandler<RecommendtionViewModel> AddedRecommendtionEvent;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            reccomendedShoppingListModel = new ReccomendedShoppingListModel();
            recommendedShoppingListViewModel = new RecommendedShoppingListViewModel(reccomendedShoppingListModel);
            recommendedShoppingListPage = new ShoppingListPage(recommendedShoppingListViewModel);

            recommendedShoppingListViewModel.AddedRecommendtionEvent += addedRecommendtionHandler;
            CreateProductCommand = new CreateProductCommand(recommendedShoppingListViewModel);

            signInViewModel = new SignInViewModel();
            signInPage = new SignInPage(signInViewModel);

            signInViewModel.SignInSuccessEvent += SignInSuccess;

            GoToRecommendedShoppingListPageCommand = new GoToRecommendedShoppingListPageCommand(mainWindow);
            GoToHomePageCommand = new GoToHomePageCommand(mainWindow);
            GoToMyShoppingListPageCommand = new GoToMyShoppingListPageCommand(mainWindow);
            GoToShoppingHistoryPageCommand = new GoToShoppingHistoryPageCommand(mainWindow);
            GoToStatisticsPageCommand = new GoToStatisticsPageCommand(mainWindow);
            GoToSignInPageCommand = new GoToSignInPageCommand(mainWindow);


            
        }

        private void SignInSuccess(object sender, Consumer consumer)
        {
            GoToHomePageCommand.Execute(null);
        }

        private async void addedRecommendtionHandler(object sender, OrderedProduct orderedProduct)
        {
            RecommendtionViewModel recommendtionViewModel = new RecommendtionViewModel(orderedProduct);

            addRecommendation(recommendtionViewModel);
        }

        public ObservableCollection<RecommendtionViewModel> RecommendedProducts { get; set; }

        public async void addRecommendation(RecommendtionViewModel recommendtionViewModel)
        {
            AddedRecommendtionEvent.Invoke(this, recommendtionViewModel);
        }

    }
}
