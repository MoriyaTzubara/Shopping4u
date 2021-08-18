using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class ProductsGroup
    {
        public ProductsGroup(IEnumerable<string> _products)
        {
            products = _products;
        }
        private IEnumerable<string> products;
        public string productsShortStr 
        { 
            get 
            {
                return (productsFulltStr.Length > 30) ? productsFulltStr.Substring(0, 30) + "..." : productsFulltStr; 
            } 
            set { } 
        }
        public string productsFulltStr { get { return String.Join(", ", products.ToArray()); } set { } }
    }
    public class ProductsGroupViewModel
    {
        public IEnumerable<ProductsGroup> ProductsGroup { get; set; }
        public ProductsGroupViewModel()
        {
            ProductsGroup = new List<ProductsGroup>()
            {
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
                new ProductsGroup(new List<string>() {"Coffee", "Water", "Milk", "Sugar", "Cups"}),
            };
        }
    }
}
