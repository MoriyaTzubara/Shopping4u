using BE;
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
        Consumer GetConsumer(int consumerId);
        ShoppingList GetShoppingList(int shoppingListId);
        List<ShoppingList> GetConsumerHistory(int consumerId);
        List<string> GetBranchesNameInList(int shoppingListId);
        string GetBranchName(int branchId);
        string GetProductName(int productId);
        double GetTotalOfShoppingList(int shoppingListId);
        string[] GetShoppingListsOfConsumer(int consumerId);
        string[] GetShoppingLists();
        IEnumerable<string> GetProductsIdInList();
        string GetProductsIdOfList(int shoppingListId);
        IEnumerable<string> GetProductsNamesInList();
        List<string> GetBranchesNameOfSpecificProduct(int productId);
        List<string> GetProductsNameOfSpecificBranch(int branchId);
        List<string> GetCategoriesNames();
        #endregion
        #region INSERT
        ShoppingList CreateUnapprovedShoppingList(int consumerId);
        void InsertApprovedShoppingList(ShoppingList shoppingList);
        void InsertOrderedProducts(List<OrderedProduct> orderedProducts,int shoppingListId);
        void InsertOrderedProduct(OrderedProduct orderedProduct);
        void InsertBaseProduct(Product product);
        Branch InsertBranch(Branch branch);
        BranchProduct InsertBranchProduct(Product product,Branch branch, double price);
        void InsertConsumer(Consumer consumer);
        #endregion
        #region UPDATE
        void UpdateProductPicture(string url, int productId);
        void UpdateOrderedProduct(int quantity, int shoppingListId, int branchProductId);
        void UpdateShoppingList(int shoppingListId);
        #endregion
        #region DELETE
        void DeleteOrderedProduct(int shoppingListId, int branchProductId);
        void DeleteUnapprovedShoppingList(int consumerId);
        #endregion
        #region FILTERS
        Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId);
        Dictionary<DateTime, double> ShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId);
        Product GetProductByName(string name);
        List<OrderedProduct> FilterByBranches(List<string> branchesNames, int shoppingListId);
        IDictionary<string, List<string>> GetUsualShoppingsForEachDay(int consumerId, double minPrecent = 0.3);
        IDictionary<DateTime, double> GetShoppingsInBranchBetweenTwoDates(DateTime start, DateTime end, int consumerId, int BranchId);
        IDictionary<DateTime, double> GetShoppingsInCategoryBetweenTwoDates(DateTime start, DateTime end, int consumerId, int categoryName);
        #endregion
        #region APRIORI
        IDictionary<int, int> GetSupportOfEachItem();
        IDictionary<List<Product>, double> ProductsThatGoTogether(double minConfidence = 0.01);
        #endregion
    }
}
