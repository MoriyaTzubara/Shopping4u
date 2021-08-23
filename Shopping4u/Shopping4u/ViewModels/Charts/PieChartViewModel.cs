using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace Shopping4u.ViewModels
{
    public class PieChartViewModel
    {
        #region PROPERTIRES
        public SeriesCollection SeriesCollection { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public PieChartViewModel(PieChartModel pieChartModel)
        {
            SeriesCollection = new SeriesCollection();

            foreach (TitleValue item in pieChartModel.Data)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.title,
                    Values = new ChartValues<double> {item.value }
                });
            };
        }
        #endregion
    }

    public class TitleValue
    {
        #region PROPERTIRES
        public string title;
        public double value;
        #endregion
    }
}
