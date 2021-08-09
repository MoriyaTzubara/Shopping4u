using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class ProductsStatisticModel : StatisticModel
    {
        public string Title { get; set; }
        public IEnumerable<double> Data { get; set; }

        public ProductsStatisticModel()
        {
            Title = "Products History";
            Data = getData();
        }

        private IEnumerable<double> getData()
        {
            return new List<double>() { 98.4, 50.7, 20.99, 10.0, 65.0, 100.0 };
        }

    }
}
