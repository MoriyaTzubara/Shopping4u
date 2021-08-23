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
        #region PROPERTIRES
        string Title { get; set; }
        Dictionary<string, double> Data { get; set; }
        SeriesCollection SeriesCollection { get; set; }
        string[] Labels { get; set; }
        IEnumerable<object> Options { get; set; }
        AggregateBy AggregateBy { get; set; }
        object CurrentOption { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        #endregion
        #region COMMANDS
        SelectOptionCommand SelectOptionCommand { get; set; }
        #endregion
        #region FUNCTIONS
        void selectOption(object option);
        void selectDates(DateTime start, DateTime end);
        void setSeriesCollection(Dictionary<string, double> data, AggregateBy aggregateBy);
        void updateSeriesCollection(DateTime startDate, DateTime endDate, AggregateBy aggregateBy);
        #endregion
    }
}
