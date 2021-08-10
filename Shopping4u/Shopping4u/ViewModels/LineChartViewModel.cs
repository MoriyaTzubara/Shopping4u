using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;

namespace Shopping4u.ViewModels
{
    public class LineChartViewModel: ChartViewModel<double>
    {

        public LineChartViewModel(LineChartModel lineChartModel) : base(lineChartModel)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(Data),
                }
            };
        }


    }
}
