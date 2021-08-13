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
        public MyShoppingListViewModel(MyShoppingListModel myShoppingListModel): base(myShoppingListModel)
        {
            Title = "My Shopping List";
            CreateProductViewModel = new CreateProductViewModel() { CanScanQRCode = true };

            SaveShoppingListCommand = new SaveShoppingListCommand(this);

            ScanQRCodeCommand = new ScanQRCodeCommand(this);
        }

        public SaveShoppingListCommand SaveShoppingListCommand { get; set; }
        
        public ScanQRCodeCommand ScanQRCodeCommand { get; set; }

        public override void CreateProduct(OrderedProduct orderedProduct)
        {
            // needs to get the source of the image of the barcode
            base.CreateProduct(orderedProduct);
            MessageBox.Show("CreateProduct @ MyShoppingList");
        }
        public override void UpdateProduct(OrderedProduct orderedProduct)
        {
            base.UpdateProduct(orderedProduct);
            MessageBox.Show("UpdateProduct @ MyShoppingList");
        }
        public override void DeleteProduct(int productId)
        {
            //I need to  get shoppingListId and BranchProductId, or orderedProduct if it is more easier 
            //bl.DeleteOrderedProduct(productId);
            base.DeleteProduct(productId);
            MessageBox.Show("DeleteProduct @ MyShoppingList");
        }

        public void ScanQRCode()
        {
            string imgUrl = "";
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == DialogResult.OK)
            {
                imgUrl = of.FileName;
            }
            
            MessageBox.Show($"SCAN QR COde {imgUrl}");
        }
        public void SaveShoppingList()
        {
            MessageBox.Show("Save");
        }
    }
}
