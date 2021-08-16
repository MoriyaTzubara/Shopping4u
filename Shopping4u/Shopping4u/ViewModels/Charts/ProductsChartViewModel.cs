using BE;
using LiveCharts;
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
        }

        public Dictionary<string, double> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public IEnumerable<object> Options { get; set; }

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

        public void setSeriesCollection()
        {
            // TODO //
        }
    }
}
