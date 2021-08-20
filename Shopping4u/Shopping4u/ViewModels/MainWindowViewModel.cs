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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shopping4u.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private MainWindow mainWindow;
        private ReccomendedShoppingListModel reccomendedShoppingListModel;
        private RecommendedShoppingListViewModel recommendedShoppingListViewModel;
        private SignInViewModel signInViewModel;

        public HomePage homePage = new HomePage();
        public ShoppingListPage recommendedShoppingListPage;
        public ShoppingListPage myShoppingListPage;
        public ShoppingHistoryPage shoppingHistoryPage;
        public SignInPage signInPage;
        public ChartsPage chartsPage;


        private GoToRecommendedShoppingListPageCommand goToRecommendedShoppingListPageCommand;
        public GoToRecommendedShoppingListPageCommand GoToRecommendedShoppingListPageCommand
        {
            get { return goToRecommendedShoppingListPageCommand; }
            set { goToRecommendedShoppingListPageCommand = value; OnPropertyChanged(); }
        }

        private GoToHomePageCommand goToHomePageCommand;
        public GoToHomePageCommand GoToHomePageCommand
        {
            get { return goToHomePageCommand; }
            set { goToHomePageCommand = value; OnPropertyChanged(); }
        }

        public GoToMyShoppingListPageCommand goToMyShoppingListPageCommand;
        public GoToMyShoppingListPageCommand GoToMyShoppingListPageCommand
        {
            get { return goToMyShoppingListPageCommand; }
            set { goToMyShoppingListPageCommand = value; OnPropertyChanged(); }
        }

        public GoToShoppingHistoryPageCommand goToShoppingHistoryPageCommand;
        public GoToShoppingHistoryPageCommand GoToShoppingHistoryPageCommand
        {
            get { return goToShoppingHistoryPageCommand; }
            set { goToShoppingHistoryPageCommand = value; OnPropertyChanged(); }
        }

        public GoToStatisticsPageCommand goToStatisticsPageCommand;
        public GoToStatisticsPageCommand GoToStatisticsPageCommand
        {
            get { return goToStatisticsPageCommand; }
            set { goToStatisticsPageCommand = value; OnPropertyChanged(); }
        }

        public GoToSignInPageCommand goToSignInPageCommand;
        public GoToSignInPageCommand GoToSignInPageCommand
        {
            get { return goToSignInPageCommand; }
            set { goToSignInPageCommand = value; OnPropertyChanged(); }
        }

        public CreateProductCommand createProductCommand;
        public CreateProductCommand CreateProductCommand
        {
            get { return createProductCommand; }
            set { createProductCommand = value; OnPropertyChanged(); }
        }


        public event EventHandler<RecommendtionViewModel> AddedRecommendtionEvent;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            

            signInViewModel = new SignInViewModel();
            signInPage = new SignInPage(signInViewModel);

            signInViewModel.SignInSuccessEvent += SignInSuccess;
            signInViewModel.SignInSuccessEvent += mainWindow.SignInSuccess;


            GoToSignInPageCommand = new GoToSignInPageCommand(mainWindow);            
        }

        private void SignInSuccess(object sender, Consumer consumer)
        {
            myShoppingListPage = new ShoppingListPage(new MyShoppingListViewModel(new MyShoppingListModel()));
            shoppingHistoryPage = new ShoppingHistoryPage();
            chartsPage = new ChartsPage();
            reccomendedShoppingListModel = new ReccomendedShoppingListModel();
            recommendedShoppingListViewModel = new RecommendedShoppingListViewModel(reccomendedShoppingListModel);
            recommendedShoppingListPage = new ShoppingListPage(recommendedShoppingListViewModel);

            recommendedShoppingListViewModel.AddedRecommendtionEvent += addedRecommendtionHandler;
            CreateProductCommand = new CreateProductCommand(recommendedShoppingListViewModel);

            GoToRecommendedShoppingListPageCommand = new GoToRecommendedShoppingListPageCommand();
            GoToHomePageCommand = new GoToHomePageCommand(mainWindow);
            GoToMyShoppingListPageCommand = new GoToMyShoppingListPageCommand(mainWindow);
            GoToShoppingHistoryPageCommand = new GoToShoppingHistoryPageCommand(mainWindow);
            GoToStatisticsPageCommand = new GoToStatisticsPageCommand(mainWindow);

            homePage.ConsumerName = consumer.firstName;
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
