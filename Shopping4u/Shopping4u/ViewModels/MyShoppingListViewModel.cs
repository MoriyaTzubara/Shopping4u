using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shopping4u.Extensions;
using BE;

namespace Shopping4u.ViewModels
{
    class MyShoppingListViewModel : ShoppingListViewModel
    {
        public override string GetTitle()
        {
            return "My Shopping List";
        }
        public override IEnumerable<ProductViewModel> GetProducts()
        {
            // SHOULD BE DELETED
            return BlMock.getProducts();
        }
        
        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            MessageBox.Show($"Command parameter: {orderedProduct.getOrElae("null")}");

            MessageBox.Show("CreateProduct @ MyShoppingList");
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            MessageBox.Show($"Command parameter: {orderedProduct.getOrElae("null")}");
            MessageBox.Show("UpdateProduct @ MyShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            MessageBox.Show($"Command parameter: {productId.getOrElae("null")}");
            MessageBox.Show("DeleteProduct @ MyShoppingList");
        }

        // SHOULD BE DELETED
        private static class BlMock
        {
            static Random random = new Random();
            
            static public List<ProductViewModel> getProducts()
            {
                return  new List<ProductViewModel>();
            }

        }

    }
}
