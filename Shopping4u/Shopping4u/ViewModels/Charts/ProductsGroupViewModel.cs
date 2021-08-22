using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models.Charts;

namespace Shopping4u.ViewModels.Charts
{
    public class ProductsGroupViewModel
    {
        ProductsGroupModel productsGroupModel;
        public List<ProductsGroup> ProductsGroups { get; set; }

        public ProductsGroupViewModel()
        {
            productsGroupModel = new ProductsGroupModel();
            var groups =  productsGroupModel.getProductsGroup();
            ProductsGroups = new List<ProductsGroup>();
            foreach(var group in groups)
            {
                    ProductsGroups.Add(new ProductsGroup($"{group.Key}%", string.Join("\n", group.Value.Select(x => $"* {x.Key} -> {x.Value}"))));
            }
        }        
    }

    public class ProductsGroup
    {
        public ProductsGroup()
        {
            Probability = "";
            Groups = "";
        }
        public ProductsGroup(string probability, string groups)
        {
            Probability = probability;
            Groups = groups;

        }
        public string Probability { get; set; }
        public string Groups { get; set; }
    }

}
