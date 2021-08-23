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
        #region PROPERTIRES
        public OrderedProduct OrderedProduct { get; set; }
        #endregion
        #region CONSTRUCTOR
        public RecommendtionModel(OrderedProduct product)
        {
            this.OrderedProduct = product;
        }
        #endregion
    }
}
