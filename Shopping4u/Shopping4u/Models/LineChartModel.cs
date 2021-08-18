using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public interface ILineChartModel<T,J>
    {
        Dictionary<string, double> getData(J productId, AggregateBy aggregateBy, DateTime endDate);
        IEnumerable<T> getOption();
    }
}
