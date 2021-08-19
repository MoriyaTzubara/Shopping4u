using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public class ShoppingHistoryViewModel: INotifyPropertyChanged
    {
        public IEnumerable<HistoryShoppingListViewModel> ShoppingLists { get; set; }

        private IEnumerable<OrderedProductViewModel> products;
        public IEnumerable<OrderedProductViewModel> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }

        public ViewShoppingListCommand ViewShoppingListCommand { get; set; }
        public CloseViewShoppingListCommand CloseViewShoppingListCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void CloseViewShoppingList()
        {
            ViewListVisibility = "Collapsed";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ShoppingHistoryViewModel()
        {
            IBL bl = new BL.BL();
            ShoppingLists = bl.GetConsumerHistory(App.Consumer.id).Select(x => new HistoryShoppingListViewModel(x)).ToList();
            ViewShoppingListCommand = new ViewShoppingListCommand(this);
            CloseViewShoppingListCommand = new CloseViewShoppingListCommand(this);
            ViewListVisibility = "Collapsed";
        }

        private string viewListVisibility;
        public string ViewListVisibility 
        { 
            get { return viewListVisibility; }
            set { viewListVisibility = value; OnPropertyChanged(); }
        }

        public void ViewShoppingList(int id)
        {
            Products = ShoppingLists.FirstOrDefault(x => x.Id == id).ShoppingList.products.Select(x => new OrderedProductViewModel(x, false)).ToList();
            ViewListVisibility = "Visible";
        }
    }

    public class HistoryShoppingListViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string TotalPrice { get; set; }
        public string NumbersOfProducts { get; set; }

        public ShoppingList ShoppingList { get; set; }

        public HistoryShoppingListViewModel(ShoppingList shoppingList)
        {
            ShoppingList = shoppingList;

            Id = shoppingList.id;
            Date = shoppingList.date.ToString("dd/MM/yy");
            TotalPrice = $"{shoppingList.products.Sum(x => x.unitPrice * x.quantity)}$";
            NumbersOfProducts = $"{shoppingList.products.Count}";

        }


    }

}
