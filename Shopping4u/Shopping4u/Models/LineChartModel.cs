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
        #region FUNCTIONS
        Dictionary<string, double> getData(J productId, AggregateBy aggregateBy,DateTime startDate, DateTime endDate);
        IEnumerable<T> getOption();
        #endregion
    }
}
