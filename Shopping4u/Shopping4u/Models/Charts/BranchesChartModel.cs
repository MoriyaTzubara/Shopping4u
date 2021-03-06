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
        #region GET_DATA
        public Dictionary<string, double> getData(int branchId, AggregateBy aggregateBy,DateTime startDate, DateTime endDate)
        {
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.BranchBetweenTwoDatesByMonth(startDate, endDate, App.Consumer.id, branchId);
                case AggregateBy.WEEK:
                    return bl.BranchBetweenTwoDatesByWeek(startDate, endDate, App.Consumer.id, branchId);
                case AggregateBy.DAY:
                    return bl.BranchBetweenTwoDatesByDay(startDate, endDate, App.Consumer.id, branchId);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }
        public IEnumerable<Branch> getOption()
        {
            IBL bl = new BL.BL();
            return bl.GetBranches();
        }
        #endregion
    }
}
