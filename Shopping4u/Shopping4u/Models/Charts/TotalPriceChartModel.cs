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
        private Dictionary<string, double> result;
    public IEnumerable<double> getData()
        {
            // TODO //
            IBL bl = new BL.BL();
            result = bl.ShoppingsBetweenTwoDatesByMonth(DateTime.Now.AddYears(-1), DateTime.Now, App.Consumer.id);
            return result.OrderBy(k => Convert.ToDateTime(k.Key)).Select(k => k.Value);
        }

        public string[] getLabels()
        {
            return result.Keys.Select(k => Convert.ToDateTime(k).ToString("MMMM")).ToArray();
        }
    }
}
