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
    public class ProductsChartModel : ILineChartModel<Product>
    {
        public Dictionary<string, double> getData(int productId, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            IBL bl = new BL.BL();
            return new Dictionary<string, double>();
            //switch (aggregateBy)
            //{
            //    case AggregateBy.MONTH:
            //        return bl.ProductBetweenTwoDatesByMonth(startDate, endDate, 123, productId);
            //    case AggregateBy.WEEK:
            //        return bl.ProductBetweenTwoDatesByWeek(startDate, endDate, 123, productId);
            //    case AggregateBy.DAY:
            //        return bl.ProductBetweenTwoDatesByDay(startDate, endDate, 123, productId);
            //    default:
            //        break;
            //}
        }

        public IEnumerable<Product> getOption()
        {
            // TODO //
            IBL bl = new BL.BL();
            return new List<Product>();

        }
    }
}
