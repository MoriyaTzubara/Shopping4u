using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.ViewModels;

namespace Shopping4u.Models
{
    public class PieChartModel: IChartModel<TitleValue>
    {
        #region PROPERTIRES
        public string Title { get; set; }
        public IEnumerable<TitleValue> Data { get; set; }
        #endregion
        #region CONSTRUCTOR
        public PieChartModel() {}
        public PieChartModel(string title, IEnumerable<TitleValue> data)
        {
            Title = title;
            Data = data;
        }       
        #endregion

    }
}
