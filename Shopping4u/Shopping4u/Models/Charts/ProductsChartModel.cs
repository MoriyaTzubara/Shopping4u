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
    public class ProductsChartModel : ILineChartModel<Product,int>
    {
        #region GET_DATA
        public Dictionary<string, double> getData(int productId, AggregateBy aggregateBy,DateTime startDate, DateTime endDate)
        {
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.ProductBetweenTwoDatesByMonth(startDate, endDate, App.Consumer.id, productId);
                case AggregateBy.WEEK:
                    return bl.ProductBetweenTwoDatesByWeek(startDate, endDate, App.Consumer.id, productId);
                case AggregateBy.DAY:
                    return bl.ProductBetweenTwoDatesByDay(startDate, endDate, App.Consumer.id, productId);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }
        public IEnumerable<Product> getOption()
        {
            IBL bl = new BL.BL();
            return bl.GetProducts();
        }
        #endregion
    }
}
