using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Extensions;
using BE;
using Shopping4u.Models;
using Shopping4u.Commands;
using System.Windows.Forms;

namespace Shopping4u.ViewModels
{
    public class MyShoppingListViewModel : ShoppingListViewModel
    {
        #region PROPERTIRES
        private MyShoppingListModel myShoppingListModel;
        #endregion
        #region CONSTRUCTOR
        public MyShoppingListViewModel(MyShoppingListModel myShoppingListModel): base(myShoppingListModel)
        {
            this.myShoppingListModel = myShoppingListModel;
            Title = "My Shopping List";
            IsShowSaveList = "Visible";
            CreateProductViewModel = new CreateProductViewModel() { CanScanQRCode = true };

            SaveShoppingListCommand = new SaveShoppingListCommand(this);
        }
        #endregion
        #region COMMANDS
        public SaveShoppingListCommand SaveShoppingListCommand { get; set; }
        #endregion
        #region FUNCTIONS
        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            myShoppingListModel.CreateProduct(orderedProduct);
            base.CreateProduct(orderedProduct);
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            base.UpdateProduct(orderedProduct);
        }
        public override void DeleteProduct(int orderedProductId)
        {
            myShoppingListModel.DeleteProduct(orderedProductId);
            base.DeleteProduct(orderedProductId);
        }
        public void SaveShoppingList()
        {
            myShoppingListModel.SaveShoppingList();
            MessageBox.Show("Saved successfully");
            Clean();           

        }
        private void Clean()
        {
            Products = new System.Collections.ObjectModel.ObservableCollection<OrderedProductViewModel>();
            shoppingListId = myShoppingListModel.NewShoppingList();
            TotalPrice = 0;
            NumberOfProducts = 0;
        }
        #endregion
    }
}
