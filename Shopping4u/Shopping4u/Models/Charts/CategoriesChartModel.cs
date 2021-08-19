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
    public class CategoriesChartModel : ILineChartModel<string,string>
    {
        public Dictionary<string, double> getData(string categoryName, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.CategoryBetweenTwoDatesByMonth(startDate, endDate, App.Consumer.id, categoryName);
                case AggregateBy.WEEK:
                    return bl.CategoryBetweenTwoDatesByWeek(startDate, endDate, App.Consumer.id, categoryName);
                case AggregateBy.DAY:
                    return bl.CategoryBetweenTwoDatesByDay(startDate, endDate, App.Consumer.id, categoryName);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }

        public IEnumerable<string> getOption()
        {
            // TODO //
            IBL bl = new BL.BL();
            return bl.GetCategoriesNames();
        }
    }
}
