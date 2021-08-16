using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shopping4u.Models;
using System.Threading.Tasks;
using LiveCharts.Wpf;

namespace Shopping4u.ViewModels
{
    public enum AggregateBy { MONTH=30,WEEK=7,DAY=1};
    public abstract class ChartViewModel<T>
    {
        public Dictionary<AggregateBy, Func<DateTime, DateTime>> GetEndDate = new Dictionary<AggregateBy, Func<DateTime, DateTime>>()
        { 
            {AggregateBy.MONTH, start =>  start.AddMonths(1) },
            { AggregateBy.WEEK, start => start.AddDays(7)},
            {AggregateBy.DAY, start=> start.AddDays(1)} 
        };

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<T> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public ChartViewModel(IChartModel<T> statisticModel)
        {
            Title = statisticModel.Title;
            Data = statisticModel.Data;
        }
    }
}
