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
    public interface ILineChartViewModel
    {
        Dictionary<string, double> Data { get; set; }
        SeriesCollection SeriesCollection { get; set; }
        IEnumerable<object> Options { get; set; }

        void setSeriesCollection();
    }
}
