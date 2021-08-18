using BE;
using LiveCharts;
using LiveCharts.Wpf;
using Shopping4u.Commands;
using Shopping4u.Models.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels.Charts
{
    public class BranchesChartViewModel : ILineChartViewModel, INotifyPropertyChanged
    {
        private BranchesChartModel BranchsChartModel;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public BranchesChartViewModel()
        {
            Title = "Branches";
            BranchsChartModel = new BranchesChartModel();
            Options = getOption();
            CurrentOption = Options.ElementAtOrDefault(0);

            Branch current = CurrentOption as Branch;


            EndDate = DateTime.Now;
            StartDate = DateTime.Now.AddDays(-7);

            AggregateBy = AggregateBy.WEEK;

            Data = BranchsChartModel.getData(CurrentBranch.id, AggregateBy.WEEK,DateTime.Now.AddMonths(-1), DateTime.Now);
            setSeriesCollection(Data);
            SelectOptionCommand = new SelectOptionCommand(this);
        }

        private Dictionary<string, double> data;
        public Dictionary<string, double> Data 
        { 
            get { return data; } 
            set { data = value; OnPropertyChanged(); } 
        }

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set { seriesCollection = value; OnPropertyChanged(); }
        }
        public IEnumerable<object> Options { get; set; }
        public object CurrentOption { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }

        public SelectOptionCommand SelectOptionCommand { get; set; }
        public string Title { get; set; }

        public void selectOption(object option)
        {
            int branchId = (option as Branch).id;
            Data = getData(branchId, AggregateBy,StartDate, EndDate);
            setSeriesCollection(Data);
        }
        public void selectDates(DateTime start, DateTime end)
        {

        }

        public Dictionary<string, double> getData(int branchId, AggregateBy aggregateBy,DateTime startDate, DateTime endDate)
        {
            // TODO //
            return BranchsChartModel.getData(branchId, aggregateBy,startDate, endDate);
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
