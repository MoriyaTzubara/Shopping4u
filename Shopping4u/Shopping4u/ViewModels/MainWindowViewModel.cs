using Shopping4u.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    class MainWindowViewModel
    {
        public GoToRecommendedShoppingListPageCommand GoToRecommendedShoppingListPageCommand { get; set; }
        public GoToHomePageCommand GoToHomePageCommand { get; set; }
        public GoToMyShoppingListPageCommand GoToMyShoppingListPageCommand { get; set;}
        public GoToShoppingHistoryPageCommand GoToShoppingHistoryPageCommand { get; set; }
        public GoToStatisticsPageCommand GoToStatisticsPageCommand { get; set; }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            GoToRecommendedShoppingListPageCommand = new GoToRecommendedShoppingListPageCommand(mainWindow);
            GoToHomePageCommand = new GoToHomePageCommand(mainWindow);
            GoToMyShoppingListPageCommand = new GoToMyShoppingListPageCommand(mainWindow);
            GoToShoppingHistoryPageCommand = new GoToShoppingHistoryPageCommand(mainWindow);
            GoToStatisticsPageCommand = new GoToStatisticsPageCommand(mainWindow);
        }

        public ObservableCollection<RecommendtionViewModel> RecommendedProducts { get; set; }

    }
}
