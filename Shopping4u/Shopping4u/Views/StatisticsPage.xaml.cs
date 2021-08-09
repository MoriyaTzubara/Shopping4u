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
using Shopping4u.Views;


namespace Shopping4u
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : UserControl
    {
        public StatisticsPage()
        {
            InitializeComponent();

            TotalPriceStatisticModel totalPriceStatisticModel = new TotalPriceStatisticModel();
            ProductsStatisticModel productsStatisticModel = new ProductsStatisticModel();
            CatagoryStatisticModel catagoryStatisticModel = new CatagoryStatisticModel();
            BranchesStatisticModel branchesStatisticModel = new BranchesStatisticModel();

            Panel1.Children.Add(new StatisticUserControl(new LineChartViewModel(totalPriceStatisticModel)));
            Panel2.Children.Add(new StatisticUserControl(new LineChartViewModel(productsStatisticModel)));
            Panel3.Children.Add(new StatisticUserControl(new LineChartViewModel(productsStatisticModel)));
            Panel4.Children.Add(new StatisticUserControl(new LineChartViewModel(catagoryStatisticModel)));
            Panel5.Children.Add(new StatisticUserControl(new LineChartViewModel(branchesStatisticModel)));
        }                                                
    }
}
