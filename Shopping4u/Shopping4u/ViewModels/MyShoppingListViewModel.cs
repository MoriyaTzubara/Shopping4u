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
using Shopping4u.Models;

namespace Shopping4u.ViewModels
{
    class MyShoppingListViewModel : ShoppingListViewModel
    {       

        public MyShoppingListViewModel(MyShoppingListModel myShoppingListModel): base(myShoppingListModel)
        {
            Title = "My Shopping List";
        }

        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            // needs to get the source of the image of the barcode
            MessageBox.Show("CreateProduct @ MyShoppingList");
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            MessageBox.Show("UpdateProduct @ MyShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            //I need to  get shoppingListId and BranchProductId, or orderedProduct if it is more easier 
            //bl.DeleteOrderedProduct(productId);
            MessageBox.Show("DeleteProduct @ MyShoppingList");
        }
    }
}
