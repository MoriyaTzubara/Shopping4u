using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public interface IChartModel<T>
    {
        #region PROPERTIRES
        string Title { get; set; }
        IEnumerable<T> Data {get; set;}
        #endregion

    }
}
