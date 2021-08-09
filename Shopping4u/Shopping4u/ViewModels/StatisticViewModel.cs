using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shopping4u.Models;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class StatisticViewModel
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public StatisticViewModel(StatisticModel statisticModel)
        {
            Title = statisticModel.Title;
            Data = statisticModel.Data;
        }
    }
}
