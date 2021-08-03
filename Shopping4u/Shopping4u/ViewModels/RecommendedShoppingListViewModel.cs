using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shopping4u.Extensions;

namespace Shopping4u.ViewModels
{
    class RecommendedShoppingListViewModel : ShoppingListViewModel
    {
        public static List<ProductViewModel> products;
        public override string GetTitle()
        {
            return "Recommended Shopping List";
        }
        public override bool IsReadOnly()
        {
            return false;
        }
        public override IEnumerable<ProductViewModel> GetProducts()
        {
            IBL bl = new BL.BL();
            if(products == null)
                products = bl.GetProducts().Select(p => new ProductViewModel(new OrderedProduct())).ToList();
            return products;
        }

        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            MessageBox.Show($"Command parameter: {orderedProduct.getOrElae("null")}");

            MessageBox.Show("CreateProduct @ RecommendedShoppingList");
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            MessageBox.Show($"Command parameter: {orderedProduct.getOrElae("null")}");
            MessageBox.Show("UpdateProduct @ RecommendedShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            MessageBox.Show($"Command parameter: {productId.getOrElae("null")}");
            MessageBox.Show("DeleteProduct @ RecommendedShoppingList");
        }


        // SHOULD BE DELETED
        private static class BlMock
        {
            static Random random = new Random();

            static public List<ProductViewModel> getProducts()
            {
                return new List<ProductViewModel>();
            }

        }

    }
}
