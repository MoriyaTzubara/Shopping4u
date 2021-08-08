using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shopping4u.Extensions;
using Shopping4u.Models;

namespace Shopping4u.ViewModels
{
    public class RecommendedShoppingListViewModel : ShoppingListViewModel
    {
        public RecommendedShoppingListViewModel(ReccomendedShoppingListModel reccomendedShoppingListModel): base(reccomendedShoppingListModel)
        {
            Title = "Reccomended Shopping List";
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

    }
}
