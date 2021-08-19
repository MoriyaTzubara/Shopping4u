using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Commands;
using Shopping4u.ViewModels.Charts;

namespace Shopping4u.ViewModels
{
    public class ChartsPageViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }
        public string AggregateStr { get { return AggregateBy.ToString(); } set { } }

        public ChartsPageViewModel()
        {
            SetAggregateByCommand = new SetAggregateByCommand(this);
            SetStartDateCommand = new SetStartDateCommand(this);
            SetEndDateCommand = new SetEndDateCommand(this);

            ProductsChartViewModel = new ProductsChartViewModel();
            BranchesChartViewModel = new BranchesChartViewModel();
            CategoriesChartViewModel = new CategoriesChartViewModel();

            AggregateBy = AggregateBy.DAY;
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;
        }

        public SetAggregateByCommand SetAggregateByCommand { get; set; }
        public SetStartDateCommand SetStartDateCommand { get; set; }
        public SetEndDateCommand SetEndDateCommand { get; set; }

        public void SetAggregateBy(AggregateBy aggregateBy)
        {
            AggregateBy = aggregateBy;
            UpdateCharts();
        }
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
            UpdateCharts();
        }
        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
            UpdateCharts();
        }
        private void UpdateCharts()
        {
            ProductsChartViewModel.updateSeriesCollection(StartDate, EndDate, AggregateBy);
            BranchesChartViewModel.updateSeriesCollection(StartDate, EndDate, AggregateBy);
            CategoriesChartViewModel.updateSeriesCollection(StartDate, EndDate, AggregateBy);
        }

        public ILineChartViewModel ProductsChartViewModel;
        public ILineChartViewModel BranchesChartViewModel;
        public ILineChartViewModel CategoriesChartViewModel;
    }
}
