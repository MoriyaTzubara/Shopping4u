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
        private CategoriesChartModel CategoriesChartModel;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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

            Data = CategoriesChartModel.getData(current, AggregateBy,StartDate, EndDate);
            setSeriesCollection(Data, AggregateBy);

            SelectOptionCommand = new SelectOptionCommand(this);
            selectOption(current);

            Labels = Data.Select(x => x.Key).ToArray();

        }

        public string Title { get; set; }

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

        private string[] labels;
        public string[] Labels { get { return labels; } set { labels = value; OnPropertyChanged(); } }


        public object CurrentOption { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }

        public SelectOptionCommand SelectOptionCommand { get; set; }

        public void selectOption(object option)
        {
            string categoryName = option.ToString();
            Data = getData(categoryName, AggregateBy, StartDate, EndDate);
            setSeriesCollection(Data, AggregateBy);
        }
        public void selectDates(DateTime start, DateTime end)
        {

        }


        public Dictionary<string, double> getData(string CategoryName, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            return CategoriesChartModel.getData(CategoryName, aggregateBy, startDate, endDate);
        }

        public IEnumerable<string> getOption()
        {
            // TODO //
            return CategoriesChartModel.getOption();

        }

        public void setSeriesCollection(Dictionary<string, double> data, AggregateBy aggregateBy)
        {
            // TODO //

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(data.Keys.OrderBy(k => TotalPriceChartModel.allMonths.ToList().IndexOf(k)).Select(k => data[k])),
                }
            };
        }

        public void updateSeriesCollection(DateTime startDate, DateTime endDate, AggregateBy aggregateBy)
        {
            StartDate = startDate;
            EndDate = endDate;
            AggregateBy = aggregateBy;

            Data = getData(CurrentOption.ToString(), aggregateBy, startDate, endDate);
            if (AggregateBy == AggregateBy.MONTH)
                Labels = data.ToList().OrderBy(k => TotalPriceChartModel.allMonths.ToList().IndexOf(k.Key)).Select(k => k.Key).ToArray();
            else
                Labels = Data.OrderBy(k => Convert.ToDateTime(k.Key)).Select(x => x.Key).ToArray();
            setSeriesCollection(Data, AggregateBy);
        }
    }
}
