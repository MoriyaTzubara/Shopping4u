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
using System.Threading;

namespace Shopping4u.ViewModels
{
    public class RecommendedShoppingListViewModel : ShoppingListViewModel
    {
        public RecommendedShoppingListViewModel(ReccomendedShoppingListModel reccomendedShoppingListModel): base(reccomendedShoppingListModel)
        {
            Title = "Recommended Shopping List";
            CreateProductViewModel = new CreateProductViewModel()
            {
                CanScanQRCode = false,
            };
        }

        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            base.CreateProduct(orderedProduct);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                tryRecommend(this.Products);
            }).Start();

            MessageBox.Show("CreateProduct @ RecommendedShoppingListViewModel");

            //products.Add(new ProductViewModel(orderedProduct));
        }

        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            base.UpdateProduct(orderedProduct);
            //It doesn't come here but still do the job well
            //int deleteIndex = products.FindIndex(o => o.ShoppingListId == orderedProduct.shoppingListId && o.BranchProductId == orderedProduct.branchProductId);
            //products[deleteIndex] =new ProductViewModel(orderedProduct);
            //MessageBox.Show($"Command parameter: {orderedProduct.getOrElse("null")}");
            //MessageBox.Show("UpdateProduct @ RecommendedShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            base.DeleteProduct(productId);

            //int deleteIndex = products.FindIndex(o => o.ShoppingListId == orderedProduct.shoppingListId && o.BranchProductId == orderedProduct.branchProductId);
            //products.RemoveAt(deleteIndex);
            MessageBox.Show($"Command parameter: {productId.getOrElse("null")}");
            MessageBox.Show("DeleteProduct @ RecommendedShoppingList");
        }

        public event EventHandler<OrderedProduct> AddedRecommendtionEvent;


        private async void tryRecommend(IEnumerable<OrderedProductViewModel> products)
        {
            Thread.Sleep(5000);

            if (products == null)
                return;

            Random random = new Random();
            int index = random.Next(0, products.ToList().Count);

            AddedRecommendtionEvent.Invoke(this, products.ElementAt(index).orderedProduct);

        }
    }
}
