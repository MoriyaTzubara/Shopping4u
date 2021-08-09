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
        public IgnoreRecommendationCommand IgnoreRecommendationCommand { get; set; }

        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }

        public int id;
        public int shoppingListId;
        public int branchProductId;
        public double unitPrice;
        public int quantity = 1;

        public RecommendtionViewModel(OrderedProduct orderedProduct)
        {

            ProductName = orderedProduct.GetProduct().name;
            ImgUrl = orderedProduct.GetProduct().imageUrl;
            IgnoreRecommendationCommand = new IgnoreRecommendationCommand();

            id = orderedProduct.id;
            shoppingListId = orderedProduct.shoppingListId;
            branchProductId = orderedProduct.branchProductId;
            unitPrice = orderedProduct.unitPrice;
            quantity = 1;
        }
    }
}
