using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class LineChartModel : StatisticModel<double>
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }

        public LineChartModel() { }
        public LineChartModel(string title, IEnumerable<double> data)
        {
            Title = title;
            Data = data;
        }
    }
}
