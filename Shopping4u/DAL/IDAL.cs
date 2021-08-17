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
        List<Branch> GetBranches();
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
        Consumer GetConsumer(string email);
        IEnumerable<string> GetProductsNamesInList();
        List<BranchProduct> GetBranchProductsOfSpecificProduct(int productId);
        List<string> GetProductsNameOfSpecificBranch(int branchId);
        List<string> GetCategoriesNames();
        string GetProductNameByBranchProductId(int branchProductId);
        #endregion
        #region INSERT
        ShoppingList CreateUnapprovedShoppingList(int consumerId);
        void InsertApprovedShoppingList(ShoppingList shoppingList);
        void InsertOrderedProducts(List<OrderedProduct> orderedProducts,int shoppingListId);
        OrderedProduct InsertOrderedProduct(OrderedProduct orderedProduct);
        void InsertBaseProduct(Product product);
        Branch InsertBranch(Branch branch);
        BranchProduct InsertBranchProduct(Product product,Branch branch, double price);
        Consumer InsertConsumer(Consumer consumer);
        #endregion
        #region UPDATE
        void UpdateProductPicture(string url, int productId);
        void UpdateOrderedProduct(OrderedProduct orderedProduct);
        void SaveShoppingList(int shoppingListId);
        #endregion
        #region DELETE
        void DeleteOrderedProduct(int orderedProductId);
        void DeleteUnapprovedShoppingList(int consumerId);
        #endregion
        #region FILTERS
        Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId);
        Product GetProductByName(string name);
        List<OrderedProduct> FilterByBranches(List<string> branchesNames, int shoppingListId);
        IDictionary<string, List<string>> GetUsualShoppingsForEachDay(int consumerId, double minPrecent = 0.3);
        IDictionary<DateTime, double> GetShoppingsInBranchBetweenTwoDates(DateTime start, DateTime end, int consumerId, int BranchId);
        IDictionary<DateTime, double> GetShoppingsInCategoryBetweenTwoDates(DateTime start, DateTime end, int consumerId, int categoryName);
        #endregion
        #region GRAPH
        Dictionary<string, double> CategoryBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, string categoryName);
        Dictionary<string, double> CategoryBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, string categoryName);
        Dictionary<string, double> CategoryBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, string categoryName);
        Dictionary<string, double> BranchBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int branchId);
        Dictionary<string, double> BranchBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int branchId);
        Dictionary<string, double> BranchBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int branchId);
        Dictionary<string, double> ProductBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int productId);
        Dictionary<string, double> ProductBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int productId);
        Dictionary<string, double> ProductBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int productId);
        Dictionary<string, double> ShoppingsBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId);
        Dictionary<string, double> ShoppingsBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId);
        Dictionary<string, double> ShoppingsBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId);
        #endregion
        #region APRIORI
        IDictionary<int, int> GetSupportOfEachItem();
        IDictionary<List<Product>, double> ProductsThatGoTogether(double minConfidence = 0.01);
        #endregion
        #region CONVERT
        int FindBranchProductIdForThisProduct(int id);
        #endregion
    }
}
