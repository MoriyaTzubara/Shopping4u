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
    public class ProductsChartViewModel : ILineChartViewModel
    {
        private ProductsChartModel productsChartModel;
        public ProductsChartViewModel()
        {
            productsChartModel = new ProductsChartModel();
            Options = getOption();
            CurrentProduct = Options.ElementAtOrDefault(0) as Product;

            Data = productsChartModel.getData(CurrentProduct.id, AggregateBy.WEEK, DateTime.Now, DateTime.Now.AddDays(7));
            setSeriesCollection(Data);
        }

        public Dictionary<string, double> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public IEnumerable<object> Options { get; set; }
        public Product CurrentProduct { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void selectOption(object option)
        {

        }
        public void selectDates(DateTime start, DateTime end)
        {

        }

        public Dictionary<string, double> getData(int productId, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
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
