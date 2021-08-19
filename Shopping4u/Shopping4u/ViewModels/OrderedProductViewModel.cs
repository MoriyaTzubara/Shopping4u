using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
using Shopping4u.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Extensions;

namespace Shopping4u.ViewModels
{
    public class OrderedProductViewModel: IProductViewModel, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public OrderedProduct orderedProduct;

        public OrderedProductViewModel(OrderedProduct orderedProduct, bool isNotReadOnly = true)
        {
            this.orderedProduct = orderedProduct;

            UpdateQuantityCommand = new UpdateQuantityCommand(this);

            Id = orderedProduct.id;
            ShoppingListId = orderedProduct.shoppingListId;
            Quantity = orderedProduct.quantity;
            UnitPrice = orderedProduct.unitPrice;
            BranchProductId = orderedProduct.branchProductId;
            ImgUrl = orderedProduct.GetProduct().imageUrl;
            BranchName = orderedProduct.GetBranch().name;
            ProductName = orderedProduct.GetProduct().name;
            IsNotReadOnly = isNotReadOnly;
        }


        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }


        public int Id { get; set; }
        
        public int ShoppingListId { get; set; }
        
        private int quantity;
        public int Quantity
        {
            get { return quantity;}
            set { quantity = value; OnPropertyChanged();}
        }        
        
        public double UnitPrice { get; set; }
        
        public int BranchProductId { get; set; }

        public string ImgUrl { get; set; }

        public string BranchName { get; set; }
        
        public string ProductName { get; set; }

        public bool IsNotReadOnly { get; set; }
        public bool IsReadOnly { get { return !IsNotReadOnly; } set { } }

    }
}
