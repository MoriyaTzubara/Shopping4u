using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public interface ILineChartModel<T>
    {
        Dictionary<string, double> getData(int productId, AggregateBy aggregateBy, DateTime startDate, DateTime endDate);
        IEnumerable<T> getOption();
    }
}
