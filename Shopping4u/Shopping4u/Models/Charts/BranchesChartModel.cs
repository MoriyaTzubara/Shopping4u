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
    public class BranchesChartModel : ILineChartModel<Branch,int>
    {
        public Dictionary<string, double> getData(int branchId, AggregateBy aggregateBy, DateTime endDate)
        {
            // TODO //
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.BranchBetweenTwoDatesByMonth(endDate.AddYears(-1), endDate, 1, branchId);
                case AggregateBy.WEEK:
                    return bl.BranchBetweenTwoDatesByWeek(endDate.AddMonths(-1), endDate, 1, branchId);
                case AggregateBy.DAY:
                    return bl.BranchBetweenTwoDatesByDay(endDate.AddDays(-7), endDate, 1, branchId);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }

        public IEnumerable<Branch> getOption()
        {
            // TODO //
            IBL bl = new BL.BL();
            return bl.GetBranches();
        }
    }
}
