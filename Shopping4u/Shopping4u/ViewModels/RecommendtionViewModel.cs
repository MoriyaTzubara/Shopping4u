using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;
using Shopping4u.Commands;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.ViewModels
{
    public class RecommendtionViewModel
    {
        #region CONSTRUCTOR
        public RecommendtionViewModel(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;

            ProductName = orderedProduct.GetProduct().name;
            ImgUrl = orderedProduct.GetProduct().imageUrl;
            IgnoreRecommendationCommand = new IgnoreRecommendationCommand();
        }
        #endregion

        #region COMMANDS
        public IgnoreRecommendationCommand IgnoreRecommendationCommand { get; set; }
        #endregion

        #region PROPERTIRES
        public OrderedProduct orderedProduct { get; set; }
        
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        #endregion

    }
}
