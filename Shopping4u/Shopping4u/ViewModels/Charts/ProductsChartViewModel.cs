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
    public class ProductsChartViewModel : ILineChartViewModel, INotifyPropertyChanged
    {
        private ProductsChartModel productsChartModel;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ProductsChartViewModel()
        {
            Title = "Products";

            productsChartModel = new ProductsChartModel();
            Options = getOption();
            CurrentOption = Options.ElementAtOrDefault(0);

            Product current = CurrentOption as Product;

            EndDate = DateTime.Now;
            StartDate = DateTime.Now.AddMonths(-1);

            AggregateBy = AggregateBy.DAY;

            Data = productsChartModel.getData(current.id, AggregateBy.DAY, StartDate, EndDate);
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
        public object CurrentOption { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }

        public SelectOptionCommand SelectOptionCommand { get; set; }

        private string[] labels;
        public string[] Labels { get { return labels; } set { labels = value; OnPropertyChanged(); } }



        public void selectOption(object option)
        {
            int productId = (option as Product).id;
            Data = getData(productId, AggregateBy, StartDate, EndDate);
            setSeriesCollection(Data, AggregateBy);
        }
        public void selectDates(DateTime start, DateTime end)
        {

        }

        public Dictionary<string, double> getData(int productId, AggregateBy aggregateBy,DateTime startDate, DateTime endDate)
        {
            // TODO //
            return productsChartModel.getData(productId, aggregateBy, startDate, endDate);
        }

        public IEnumerable<Product> getOption()
        {
            // TODO //
            return productsChartModel.getOption();

        }

        public void setSeriesCollection(Dictionary<string, double> data, AggregateBy aggregateBy)
        {
            // TODO //
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(data.Keys.OrderBy(k => ShoppingList.allMonths.ToList().IndexOf(k)).Select(k => data[k])),
                }
            };
        }

        public void updateSeriesCollection(DateTime startDate, DateTime endDate, AggregateBy aggregateBy)
        {
            StartDate = startDate;
            EndDate = endDate;
            AggregateBy = aggregateBy;

            Data = getData((CurrentOption as Product).id, aggregateBy, startDate, endDate);
            if (AggregateBy == AggregateBy.MONTH)
                Labels = data.ToList().OrderBy(k => ShoppingList.allMonths.ToList().IndexOf(k.Key)).Select(k => k.Key).ToArray();
            else
                Labels = Data.OrderBy(k => Convert.ToDateTime(k.Key)).Select(x => x.Key).ToArray();
            setSeriesCollection(Data, AggregateBy);
        }
    }
}
