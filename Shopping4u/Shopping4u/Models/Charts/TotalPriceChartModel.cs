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
        public IEnumerable<double> getData()
        {
            // TODO //
            IBL bl = new BL.BL();
            return new List<double>() { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };
        }

        public string[] getLabels()
        {
            return new string[] { "January", "February", "March", "April", "May", "June", "July", "August"};
        }
    }
}
