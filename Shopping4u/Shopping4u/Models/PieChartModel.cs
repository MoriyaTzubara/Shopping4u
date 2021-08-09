using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.ViewModels;

namespace Shopping4u.Models
{
    public class PieChartModel: StatisticModel<TitleValue>
    {
        public string Title { get; set; }
        public IEnumerable<TitleValue> Data { get; set; }

        public PieChartModel() 
        {
            Title = "";
            Data = new List<TitleValue>();
        }
        public PieChartModel(string title, IEnumerable<TitleValue> data)
        {
            Title = title;
            Data = data;
        }
    }
}
