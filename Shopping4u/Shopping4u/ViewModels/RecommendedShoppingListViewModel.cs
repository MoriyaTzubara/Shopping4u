using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    class RecommendedShoppingListViewModel : ShoppingListViewModel
    {
        public RecommendedShoppingListViewModel()
        {


            Title = "Recommended Shopping List";
            readOnly = false;

            // SHOULD BE DELETED
            Products = BlMock.getProducts();
        }


        // SHOULD BE DELETED
        private static class BlMock
        {
            static Random random = new Random();

            static public List<ProductViewModel> getProducts()
            {
                return new List<ProductViewModel>()
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
                    ProductName = "Meat",
                    Quantity = random.Next(1, 20)
                },

            };
            }

        }

    }
}
