using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public interface StatisticModel
    {
        string Title { get; set; }
        IEnumerable<double> Data {get; set;}
    }
}
