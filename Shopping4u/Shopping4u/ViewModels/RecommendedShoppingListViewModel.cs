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
        #region CONSTRUCTOR
        public RecommendedShoppingListViewModel(ReccomendedShoppingListModel reccomendedShoppingListModel): base(reccomendedShoppingListModel)
        {
            Title = "Recommended Shopping List";
            CreateProductViewModel = new CreateProductViewModel()
            {
                CanScanQRCode = false,
            };
            IsShowSaveList = "Collapsed";
        }
        #endregion
        #region FUNCTIONS
        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            base.CreateProduct(orderedProduct);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                tryRecommend(this.Products);
            }).Start();


        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            base.UpdateProduct(orderedProduct);
        }
        public override void DeleteProduct(int productId)
        {
            base.DeleteProduct(productId);
        }
        private async void tryRecommend(IEnumerable<OrderedProductViewModel> products)
        {
            Thread.Sleep(5000);

            if (products == null)
                return;

            IBL bl = new BL.BL();
            List<Product> result = bl.AprioriRecommender(products.ToList().Select(p => p.orderedProduct).ToList(),0.3,0.65).ToList();
            if(result.Count() != 0)
                AddedRecommendtionEvent.Invoke(this, bl.ConvertProductToOrderedProduct(result.ToList()[0]));

        }
        #endregion
        #region EVENTS
        public event EventHandler<OrderedProduct> AddedRecommendtionEvent;
        #endregion
    }
}
