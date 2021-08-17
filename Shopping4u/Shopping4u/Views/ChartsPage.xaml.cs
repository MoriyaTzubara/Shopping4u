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
using Shopping4u.ViewModels.Charts;

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

            ILineChartViewModel productsChartViewModel = new ProductsChartViewModel();

            Panel1.Children.Add(new CartesianChartUserControl());

            Panel2.Children.Add(new LineChartUserControl(productsChartViewModel));
            
            //Panel3.Children.Add(new );

            //Panel4.Children.Add(new );

            //Panel5.Children.Add(new );

        }                                                
    }
}
