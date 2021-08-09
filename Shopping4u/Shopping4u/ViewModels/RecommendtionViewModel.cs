using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;
using Shopping4u.Extensions;

namespace Shopping4u.ViewModels
{
    public class RecommendtionViewModel
    {
        public RecommendtionModel recommendtionModel;

        public string ProductName { get; set; }
        public string ImgUrl { get; set; }

        public RecommendtionViewModel(RecommendtionModel recommendtionModel)
        {
            this.recommendtionModel = recommendtionModel;

            ProductName = recommendtionModel.OrderedProduct.GetProduct().name;
            ImgUrl = recommendtionModel.OrderedProduct.GetProduct().imageUrl;
        }
    }
}
