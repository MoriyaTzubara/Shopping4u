using BE;
using LiveCharts;
using LiveCharts.Wpf;
using Shopping4u.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels.Charts
{
    public class BranchesChartViewModel : ILineChartViewModel
    {
        private BranchesChartModel BranchsChartModel;
        public BranchesChartViewModel()
        {
            BranchsChartModel = new BranchesChartModel();
            Options = getOption();
            CurrentBranch = Options.ElementAtOrDefault(0) as Branch;

            Data = BranchsChartModel.getData(CurrentBranch.id, AggregateBy.WEEK, DateTime.Now, DateTime.Now.AddDays(7));
            setSeriesCollection(Data);
        }

        public Dictionary<string, double> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public IEnumerable<object> Options { get; set; }
        public Branch CurrentBranch { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void selectOption(object option)
        {

        }
        public void selectDates(DateTime start, DateTime end)
        {

        }

        public Dictionary<string, double> getData(int branchId, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            return BranchsChartModel.getData(branchId, aggregateBy, startDate, endDate);
        }

        public IEnumerable<Branch> getOption()
        {
            // TODO //
            return BranchsChartModel.getOption();

        }

        public void setSeriesCollection(Dictionary<string, double> data)
        {
            // TODO //

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(data.Values),
                }
            };
        }
    }
}
