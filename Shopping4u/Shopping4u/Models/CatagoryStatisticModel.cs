using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class CatagoryStatisticModel: StatisticModel
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }
        
        public CatagoryStatisticModel()
        {
            Title = "Catagories History";
            Data = getData();
        }

        private IEnumerable<double> getData()
        {
            return new List<double>() { 25.0, 10.0, 100.0, 32.0, 37.0, 0.0 };
        }

    }
}
