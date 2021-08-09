using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class BranchesStatisticModel: StatisticModel
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }
        
        public BranchesStatisticModel()
        {
            Title = "Stores History";
            Data = getData();
        }

        private IEnumerable<double> getData()
        {
            return new List<double>() { 1.0, 2.0, 3, 0, 4.0 };
        }
    }
}
