using BE;
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
            IBL bl = new BL.BL();
            // SHOULD BE DELETED
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (OrderedProduct item in bl.CreateUnapprovedShoppingList(123).products)
            {
                products.Add(new ProductViewModel(item));
            }
            return products;
        }
        
        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            // needs to get the source of the image of the barcode
            MessageBox.Show("CreateProduct @ MyShoppingList");
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            IBL bl = new BL.BL();
            bl.UpdateOrderedProduct(orderedProduct.quantity, orderedProduct.shoppingListId, orderedProduct.branchProductId);
            MessageBox.Show("UpdateProduct @ MyShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            //I need to  get shoppingListId and BranchProductId, or orderedProduct if it is more easier 
            IBL bl = new BL.BL();
            //bl.DeleteOrderedProduct(productId);
            MessageBox.Show("DeleteProduct @ MyShoppingList");
        }

        //SHOULD BE DELETED
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
