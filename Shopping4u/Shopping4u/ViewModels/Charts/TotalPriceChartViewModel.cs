using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models.Charts;

namespace Shopping4u.ViewModels.Charts
{
    public class TotalPriceChartViewModel
    {
        TotalPriceChartModel totalPriceChartModel;
        public TotalPriceChartViewModel()
        {
            totalPriceChartModel = new TotalPriceChartModel();
            setSeriesCollection();
        }

        public void setSeriesCollection()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double>(totalPriceChartModel.getData())
                }
            };

            Labels = totalPriceChartModel.getLabels();
            Formatter = value => $"{value}$";

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
