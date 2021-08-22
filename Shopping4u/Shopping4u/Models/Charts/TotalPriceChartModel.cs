using BE;
using Shopping4u.BL;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class TotalPriceChartModel
    {
        private Dictionary<string, double> data;
    public IEnumerable<double> getData()
        {
            IBL bl = new BL.BL();
            data = bl.ShoppingsBetweenTwoDatesByMonth(DateTime.Now.AddYears(-1), DateTime.Now, App.Consumer.id);
            return data.OrderBy(k => Convert.ToDateTime(k.Key)).Select(k => k.Value);
        }

        public string[] getLabels()
        {
            return data.Keys.Select(k => Convert.ToDateTime(k).ToString("MMMM")).ToArray();
        }
    }
}
