using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class TotalPriceStatisticModel : StatisticModel
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }

        public TotalPriceStatisticModel()
        {
            Title = "Total Price History";
            Data = getData();
        }

        private IEnumerable<double> getData()
        {
            return new List<double>() { 15.0, 82.0, 3, 0, 4.0 };
        }
    }
}