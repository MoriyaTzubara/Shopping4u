using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class TotalPriceChartModel
    {
        // TALYA TODO //
        public static string[] allMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public IEnumerable<double> getData()
        {
            // TODO //
            IBL bl = new BL.BL();
            //var resulttt = bl.ProductsBoughtTogether(1);
            Dictionary<string,double> result = bl.ShoppingsBetweenTwoDatesByMonth(DateTime.Now.AddYears(-1), DateTime.Now, App.Consumer.id);
            return result.Keys.OrderBy(k => allMonths.ToList().IndexOf(k)).Select(k => result[k]);
        }

        public string[] getLabels()
        {
            return allMonths;
        }
    }
}
