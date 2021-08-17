using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;
using Shopping4u.Commands;

namespace Shopping4u.ViewModels
{
    public interface ILineChartViewModel
    {
        Dictionary<string, double> Data { get; set; }
        SeriesCollection SeriesCollection { get; set; }
        IEnumerable<object> Options { get; set; }
        AggregateBy AggregateBy { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        SelectOptionCommand SelectOptionCommand { get; set; }

        void selectOption(object option);
        void selectDates(DateTime start, DateTime end);

        void setSeriesCollection(Dictionary<string, double> data);
    }
}
