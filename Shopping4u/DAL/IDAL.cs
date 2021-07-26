using BE;
using Shopping4u.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.DAL
{
    public interface IDAL
    {
        #region SELECT
        List<Product> GetProducts();
        BranchProduct GetBranchProduct(int branchProductId);
        Product GetProduct(int productId);
        Branch GetBranch(int branchId);
        List<ShoppingList> GetConsumerHistory(int consumerId);
        #endregion
        #region INSERT
        void InsertShoppingList(ShoppingList shoppingList);
        void InsertOrderedProducts(List<OrderedProduct> orderedProducts,int shoppingListId);
        void InsertOrderedProduct(OrderedProduct orderedProduct,int shoppingListId);
        void InsertBaseProduct(Product product);
        Branch InsertBranch(Branch branch);
        BranchProduct InsertBranchProduct(Product product,Branch branch, double price);
        #endregion
        #region UPDATE
        void UpdateProductPicture(string url, int productId);
        #endregion
        #region FILTERS
        Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId);
        Dictionary<DateTime, double> ShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId);
        List<Product> GetProductsByName(string name);
        #endregion
    }
}
