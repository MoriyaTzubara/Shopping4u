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
using Shopping4u.Models;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shopping4u.ViewModels;
using Shopping4u.ViewModels.Charts;
using Shopping4u.Views;

namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class ChartsPage : UserControl
    {
        public ChartsPage()
        {
            InitializeComponent();

            ChartsPageViewModel chartsPageViewModel = new ChartsPageViewModel();
            DataContext = chartsPageViewModel;

            Panel1.Children.Add(new CartesianChartUserControl(new TotalPriceChartViewModel()));
            Panel2.Children.Add(new ProductsGroupUserControl(new ProductsGroupViewModel()));

            Panel3.Children.Add(new LineChartUserControl(chartsPageViewModel.ProductsChartViewModel));
            Panel4.Children.Add(new LineChartUserControl(chartsPageViewModel.BranchesChartViewModel));
            Panel5.Children.Add(new LineChartUserControl(chartsPageViewModel.CategoriesChartViewModel));
        }                                                
    }
}
