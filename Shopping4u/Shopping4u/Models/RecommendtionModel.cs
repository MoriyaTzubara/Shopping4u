using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Shopping4u.Models
{
    public class RecommendtionModel
    { 
        public OrderedProduct OrderedProduct { get; set; }
        
        public RecommendtionModel(OrderedProduct orderedProduct)
        {
            this.OrderedProduct = orderedProduct;
        }
    }
}
