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

            Panel1.Children.Add(new CartesianChartUserControl());

            Panel2.Children.Add(new PieChartUserControl(new PieChartViewModel(new PieChartModel()
            {
                Title = "Barnches Freq",
                Data = new List<TitleValue>()
                {
                    new TitleValue(){title = "OSER-AD", value = .2},
                    new TitleValue(){title = "YEYNOT-BITAN", value = .3},
                    new TitleValue(){title = "SUPAR-PAHARM", value = .5},
                }
            })));
            
            Panel3.Children.Add(new LineChartUserControl(new LineChartViewModel(new LineChartModel()
            {
                Title = "Total Price History",
                Data = new List<double>() { 2.2, -44.5, 0.3, 67.0, 20.8 }
            })));
            
            Panel4.Children.Add(new LineChartUserControl(new LineChartViewModel(new LineChartModel()
            {
                Title = "Total Price History",
                Data = new List<double>() { 2.2, 44.5, 100.3, 180.0, 20.8 }
            })));
            
            Panel5.Children.Add(new LineChartUserControl(new LineChartViewModel(new LineChartModel()
            {
                Title = "Total Price History",
                Data = new List<double>() { 2.2, 44.5, 0.0, 67.0, 20.8 }
            })));

        }                                                
    }
}
