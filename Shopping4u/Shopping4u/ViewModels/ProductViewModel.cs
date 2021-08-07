﻿using BE;
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
    public class ProductViewModel: INotifyPropertyChanged
    {
        public UpdateQuantityCommand UpdateQuantityCommand { get; set; }

        public OrderedProduct orderedProduct { get; set; }

        public int Id
        {
            get { return orderedProduct.id; }
            set { }
        }
        public int ShoppingListId {
            get { return orderedProduct.shoppingListId; }
            set { }
        }
        
        private int quantity;
        public int Quantity { 
            get { return quantity;}
            set { 
                quantity = value;
                OnPropertyChanged();
            }
        }
        
        public string UnitPrice { 
            get { return $"{orderedProduct.unitPrice}$"; } 
            set { } 
        }
        
        public int BranchProductId {
            get{ return orderedProduct.branchProductId; }
            set { } 
        }

        public string ImgUrl
        {
            get { return orderedProduct.GetProduct().imageUrl; }
            set { }
        }

        public ProductViewModel(OrderedProduct orderedProduct)
        {
            this.orderedProduct = orderedProduct;
            this.quantity = orderedProduct.quantity;
            UpdateQuantityCommand = new UpdateQuantityCommand(this);
        }

        public String BranchName { 
            get { 
                return orderedProduct.GetBranch().name;
            }
            private set { } }

        public String ProductName {
            get {
                return orderedProduct.GetProduct().name;
            }
            set { } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
