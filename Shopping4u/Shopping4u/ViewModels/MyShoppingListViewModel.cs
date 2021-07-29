using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    class MyShoppingListViewModel : ShoppingListViewModel
    {
        //private IBL bl = new BL.BL();

        public MyShoppingListViewModel()
        {
            Random random = new Random();

            Title = "My Shopping List";
            readOnly = false;
            Products =new List<ProductViewModel>()
            {
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Milk",
                    Quantity = random.Next(1, 20)
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Milk",
                    Quantity = 5
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Milk",
                    Quantity = random.Next(1, 20)
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Milk",
                    Quantity = 5
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Potato",
                    Quantity = random.Next(1, 20)
                },                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Milk",
                    Quantity = 5
                },
                new ProductViewModel(new BE.OrderedProduct())
                {
                    BranchName = "OSHER AD",
                    ProductName = "Meat",
                    Quantity = random.Next(1, 20)
                },

            };
        }

    }
}
