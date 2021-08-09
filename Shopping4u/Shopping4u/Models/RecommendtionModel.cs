using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.ViewModels;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.Models
{
    public class RecommendtionModel
    { 
        public ProductViewModel Product { get; set; }
        public OrderedProduct orderedProduct { get; set; }
        
        public RecommendtionModel(ProductViewModel product)
        {
            this.Product = product;
            this.orderedProduct = product.orderedProduct;
        }
    }
}
