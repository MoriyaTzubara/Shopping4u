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
    public class CategoriesChartViewModel : ILineChartViewModel, INotifyPropertyChanged
    {
        #region PROPERTIRES
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private CategoriesChartModel CategoriesChartModel;
        public string Title { get; set; }
        public IEnumerable<object> Options { get; set; }
        public object CurrentOption { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }


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
        private string[] labels;
        public string[] Labels
        {
            get { return labels; }
            set { labels = value; OnPropertyChanged(); }
        }
        #endregion
        #region CONSTRUCTOR
        public CategoriesChartViewModel()
        {
            Title = "Categories";

            CategoriesChartModel = new CategoriesChartModel();
            Options = getOption();
            CurrentOption = Options.ElementAtOrDefault(0);

            string current = CurrentOption.ToString();


            EndDate = DateTime.Now;
            StartDate = DateTime.Now.AddMonths(-1);

            AggregateBy = AggregateBy.DAY;


            SelectOptionCommand = new SelectOptionCommand(this);
            selectOption(current);

            Labels = Data.OrderBy(x => Convert.ToDateTime(x.Key)).Select(x => x.Key).ToArray();

        }
        #endregion
        #region COMMANDS
        public SelectOptionCommand SelectOptionCommand { get; set; }
        #endregion
        #region GET_DATA
        public void selectOption(object option)
        {
            string categoryName = option.ToString();
            Data = getData(categoryName, AggregateBy, StartDate, EndDate);
            Labels = Data.OrderBy(k => Convert.ToDateTime(k.Key)).Select(x => x.Key).ToArray();
            setSeriesCollection(Data, AggregateBy);
        }
        public void selectDates(DateTime start, DateTime end)
        {
        }
        public Dictionary<string, double> getData(string CategoryName, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            return CategoriesChartModel.getData(CategoryName, aggregateBy, startDate, endDate);
        }
        public IEnumerable<string> getOption()
        {
            return CategoriesChartModel.getOption();

        }
        #endregion
        #region FUNCTIONS
        public void setSeriesCollection(Dictionary<string, double> data, AggregateBy aggregateBy)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(Data.Keys.OrderBy(k => Convert.ToDateTime(k)).Select(x => Data[x])),
                }
            };
        }
        public void updateSeriesCollection(DateTime startDate, DateTime endDate, AggregateBy aggregateBy)
        {
            StartDate = startDate;
            EndDate = endDate;
            AggregateBy = aggregateBy;

            Data = getData(CurrentOption.ToString(), aggregateBy, startDate, endDate);
            Labels = Data.OrderBy(k => Convert.ToDateTime(k.Key)).Select(x => x.Key).ToArray();
            setSeriesCollection(Data, AggregateBy);
        }
        #endregion
    }
}
