using BE;
using Shopping4u.BL;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class TotalPriceChartModel
    {
    public IEnumerable<double> getData()
        {
            // TODO //
            IBL bl = new BL.BL();
            Dictionary<string,double> result = bl.ShoppingsBetweenTwoDatesByMonth(DateTime.Now.AddYears(-1), DateTime.Now, App.Consumer.id);
            return result.Keys.OrderBy(k => ShoppingList.allMonths.ToList().IndexOf(k)).Select(k => result[k]);
        }

        public string[] getLabels()
        {
            return ShoppingList.allMonths;
        }
    }
}
