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
            StartDate = DateTime.Now.AddDays(-7);

            AggregateBy = AggregateBy.WEEK;

            Data = productsChartModel.getData(current.id, AggregateBy.DAY, StartDate, EndDate);
            setSeriesCollection(Data);

            SelectOptionCommand = new SelectOptionCommand(this);
            selectOption(current);
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


        public void selectOption(object option)
        {
            int productId = (option as Product).id;
            Data = getData(productId, AggregateBy, StartDate, EndDate);
            setSeriesCollection(Data);
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
