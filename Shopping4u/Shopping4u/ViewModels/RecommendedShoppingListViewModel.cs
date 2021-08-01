using BE;
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
        public static List<ProductViewModel> products;
        public override string GetTitle()
        {
            return "Recommended Shopping List";
        }
        public override bool IsReadOnly()
        {
            return false;
        }
        public override IEnumerable<ProductViewModel> GetProducts()
        {
            // SHOULD BE DELETED
            IBL bl = new BL.BL();
            if(products == null)
                products = bl.GetProducts().Select(p => new ProductViewModel(new OrderedProduct())).ToList();
            return products;
        }

        public override void CreateProduct(ProductViewModel productViewModel)
        {
            products.Add(productViewModel);
        }
        public override void UpdateProduct(int productId)
        {
            throw new NotImplementedException();
        }
        public override void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
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
