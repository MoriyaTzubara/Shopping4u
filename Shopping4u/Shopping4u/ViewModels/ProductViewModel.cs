using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class ProductViewModel: INotifyPropertyChanged
    {
        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }

        public OrderedProduct orderedProduct { get; set; }

        public int ShoppingListId {get; set;}
        private int quantity;
        public int Quantity { 
            get { return quantity;}
            set { 
                quantity = value;
                OnPropertyChanged();
            }
        }
        public double UnitPrice { get; set; }
        public int BranchProductId { get; set; }

        public ProductViewModel(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;

            ShoppingListId = orderedProduct.shoppingListId;
            UnitPrice = orderedProduct.unitPrice;
            Quantity = orderedProduct.quantity;
            BranchProductId = orderedProduct.branchProductId;

            UpdateQuantityCommand = new UpdateQuantityCommand(this);
        }

        public String BranchName { 
            get { 
                IBL bl = new BL.BL();
                return bl.GetBranchName(orderedProduct.branchProductId);
            }
            private set { } }

        public String ProductName {
            get {
                IBL bl = new BL.BL();
                return "";
                //return bl.GetProductName(Product.);
            }
            set { } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
