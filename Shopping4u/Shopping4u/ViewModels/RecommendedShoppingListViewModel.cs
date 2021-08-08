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
        List<ProductViewModel> products;
        public override string GetTitle()
        {
            return "Recommended Shopping List";
        }
        public override IEnumerable<ProductViewModel> GetProducts()
        {
            IBL bl = new BL.BL();
            var result = bl.GetRecommendedList(123);
            if (products == null && result != null)
                products = result.Select(p => new ProductViewModel(bl.ConvertProductToOrderedProduct(p))).ToList();
            return (List<ProductViewModel>)products.getOrElse(new List<ProductViewModel>());
        }

        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            Products.Add(new ProductViewModel(orderedProduct));
            MessageBox.Show("CreateProduct @ RecommendedShoppingListViewModel");

            //products.Add(new ProductViewModel(orderedProduct));
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            //It doesn't come here but still do the job well
            //int deleteIndex = products.FindIndex(o => o.ShoppingListId == orderedProduct.shoppingListId && o.BranchProductId == orderedProduct.branchProductId);
            //products[deleteIndex] =new ProductViewModel(orderedProduct);
            //MessageBox.Show($"Command parameter: {orderedProduct.getOrElse("null")}");
            //MessageBox.Show("UpdateProduct @ RecommendedShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            //int deleteIndex = products.FindIndex(o => o.ShoppingListId == orderedProduct.shoppingListId && o.BranchProductId == orderedProduct.branchProductId);
            //products.RemoveAt(deleteIndex);
            MessageBox.Show($"Command parameter: {productId.getOrElse("null")}");
            MessageBox.Show("DeleteProduct @ RecommendedShoppingList");
        }

        public override void ShowCreateProduct(bool isShow)
        {
            IsShowCreateProduct = isShow;
        }

        // SHOULD BE DELETED
        //private static class BlMock
        //{
        //    static Random random = new Random();

        //    static public List<ProductViewModel> getProducts()
        //    {
        //        return new List<ProductViewModel>();
        //    }

        //}

    }
}
