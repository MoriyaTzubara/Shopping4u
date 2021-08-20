using BE;
using BL;
using BL.Contracts;
using BL.Entities;
using Firebase.Storage;
using Shopping4u.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace Shopping4u.BL
{
    public class BL: IBL
    {
        private IDAL dal = new DAL.DAL();
        #region SIGN IN SIGN UP
        private bool ValidateConsumer(Consumer consumer)
        {
            if (dal.GetConsumer(consumer.id) != null)
                return false;
            //validate email?
            return true;
        }
        public bool SignUp(Consumer consumer)
        {
            if (ValidateConsumer(consumer))
            {
                dal.InsertConsumer(consumer);
                return true;
            }
            return false;
        }
        public Consumer GetConsumer(string email) {
            return dal.GetConsumer(email);
        }
        public bool SignIn(string email,string password)
        {
            Consumer consumer = GetConsumer(email);
            if (consumer == null ||consumer.password != password)
                return false;
            return true;
        }
        #endregion
        #region FIREBASE
        public async static Task StorePicture(string uploadUrl, string name, int productId)
        {
            var stream = File.Open(uploadUrl, FileMode.Open);

            // Construct FirebaseStorage with path to where you want to upload the file and put it there
            var task = new FirebaseStorage("shopping4u-6ddce.appspot.com")
             .Child($"{name}.jpg")
             .PutAsync(stream);

            // Track progress of the upload
            //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload is completed and get the download url
            var downloadUrl = await task;
            stream.Close();
            IBL bl = new BL();
            bl.UpdateProductPicture(downloadUrl, productId);
        }
        // encodes the barcodes
        public string EncodeBarcode(string downloadUrl)
        {
            string imageUrl = downloadUrl;
            // Install-Package ZXing.Net -Version 0.16.5
            var client = new WebClient();
            var stream = client.OpenRead(imageUrl);
            if (stream == null) return "";
            var bitmap = new Bitmap(stream);
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result == null)
                return null;
            return result.Text;
        }
        #endregion
        #region SELECT
        public List<Product> GetProducts()
        {
            return dal.GetProducts();
        }
        public List<Branch> GetBranches()
        {
            return dal.GetBranches();
        }
        public BranchProduct GetBranchProduct(int branchProductId)
        {
            return dal.GetBranchProduct(branchProductId);
        }

        public Product GetProduct(int productId)
        {
            return dal.GetProduct(productId);
        }

        public Branch GetBranch(int branchId)
        {
            return dal.GetBranch(branchId);
        }

        public Consumer GetConsumer(int consumerId)
        {
            return dal.GetConsumer(consumerId);
        }

        public List<ShoppingList> GetConsumerHistory(int consumerId)
        {
            return dal.GetConsumerHistory(consumerId);
        }
        public List<string> GetBranchesNameInList(int shoppingListId)
        {
            return dal.GetBranchesNameInList(shoppingListId);
        }

        public string GetBranchName(int branchId)
        {
            return dal.GetBranchName(branchId);
        }

        public string GetProductName(int productId)
        {
            return dal.GetProductName(productId);
        }

        public double GetTotalOfShoppingList(int shoppingListId)
        {
            return dal.GetTotalOfShoppingList(shoppingListId);
        }
        public string[] GetShoppingListsOfConsumer(int consumerId)
        {
            return dal.GetShoppingListsOfConsumer(consumerId);
        }
        public string[] GetShoppingLists()
        {
            return dal.GetShoppingLists();
        }
        public IEnumerable<string> GetProductsIdInList()
        {
            return dal.GetProductsIdInList();
        }
        public string GetProductsIdOfList(int shoppingListId)
        {
            return dal.GetProductsIdOfList(shoppingListId);
        }
        public IEnumerable<string> GetProductsNamesInList()
        {
            return dal.GetProductsNamesInList();
        }
        public ShoppingList GetShoppingList(int shoppingListId)
        {
            return dal.GetShoppingList(shoppingListId);
        }
        public List<BranchProduct> GetBranchProductsOfSpecificProduct(int productId)
        {
            return dal.GetBranchProductsOfSpecificProduct(productId);
        }
        public List<string> GetProductsNameOfSpecificBranch(int branchId)
        {
            return dal.GetProductsNameOfSpecificBranch(branchId);
        }
        public List<string> GetCategoriesNames()
        {
            return dal.GetCategoriesNames();
        }
        public string GetProductNameByBranchProductId(int branchProductId)
        {
            return dal.GetProductNameByBranchProductId(branchProductId);
        }
        #endregion
        #region INSERT  
        public ShoppingList CreateUnapprovedShoppingList(int consumerId)
        {
            return dal.CreateUnapprovedShoppingList(consumerId);
        }
        public void InsertApprovedShoppingList(ShoppingList shoppingList)
        {
            dal.InsertApprovedShoppingList(shoppingList);
        }

        public void InsertOrderedProducts(List<OrderedProduct> orderedProducts, int shoppingListId)
        {
            dal.InsertOrderedProducts(orderedProducts, shoppingListId);
        }

        public OrderedProduct EncodeOrderedProductString(string orderedProductText)
        {
            if (orderedProductText == null)
                return null;
            // id, nameOfBranch, category, nameOfProduct, price
            string[] barcodeText = orderedProductText.Split(',');
            Branch branch = new Branch
            {
                name = barcodeText[1]
            };
            Product product = new Product
            {
                id = int.Parse(barcodeText[0]),
                category = barcodeText[2],
                name = barcodeText[3]
            };
            double price = double.Parse(barcodeText[4]);
            BranchProduct branchProduct = InsertBranchProduct(product, branch, price);

            OrderedProduct orderedProduct = new OrderedProduct
            {
                branchProductId = branchProduct.branchProductId,
                shoppingListId = -1,
                unitPrice = price,
                quantity = 1
            };
            return orderedProduct;
        }
        public void InsertOrderedProduct(OrderedProduct orderedProduct)
        {
            dal.InsertOrderedProduct(orderedProduct);
        }
        public void InsertBaseProduct(Product product)
        {
            dal.InsertBaseProduct(product);
        }

        public Branch InsertBranch(Branch branch)
        {
            return dal.InsertBranch(branch);
        }

        public BranchProduct InsertBranchProduct(Product product, Branch branch, double price)
        {
            return dal.InsertBranchProduct(product, branch, price);
        }

        public Consumer InsertConsumer(Consumer consumer)
        {
            return dal.InsertConsumer(consumer);
        }
        #endregion
        #region UPDATE
        public void UpdateOrderedProduct(OrderedProduct orderedProduct)
        {
            dal.UpdateOrderedProduct(orderedProduct);
        }
        public void UpdateProductPicture(string downloadUrl, int productId)
        {
            dal.UpdateProductPicture(downloadUrl, productId);
        }
        public void SaveShoppingList(int shoppingListId)
        {
            dal.SaveShoppingList(shoppingListId);
        }
        #endregion
        #region DELETE
        public void DeleteOrderedProduct(int orderedProductId)
        {
            dal.DeleteOrderedProduct(orderedProductId);
        }
        public void DeleteUnapprovedShoppingList(int consumerId)
        {
            dal.DeleteUnapprovedShoppingList(consumerId);
        }
        #endregion
        #region FILTERS
        public Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            return dal.OrderedProductsBetweenTwoDates(start, end, consumerId);
        }

        public Product GetProductByName(string name)
        {
            return dal.GetProductByName(name);
        }
        public IEnumerable<IGrouping<int, OrderedProduct>> GroupByBranchesTheRecommendedList(List<OrderedProduct> orderedProducts)
        {
            IEnumerable<IGrouping<int,OrderedProduct>> result = from orderedProduct in orderedProducts
                         group orderedProduct by GetBranchProduct(orderedProduct.branchProductId).branchId into newGroup
                         select newGroup;
            return result;
        }
        public List<OrderedProduct> FilterByBranches(List<string> branchesNames, int shoppingListId)
        {
            return dal.FilterByBranches(branchesNames, shoppingListId);
        }
        public int FindBranchProductIdForThisProduct(int id)
        {
            return dal.FindBranchProductIdForThisProduct(id);
        }
        public OrderedProduct ConvertProductToOrderedProduct(Product product)
        {
            OrderedProduct result = new OrderedProduct();
            int branchProductId = FindBranchProductIdForThisProduct(product.id);
            result.branchProductId = branchProductId;
            result.quantity = 1;
            result.unitPrice = GetBranchProduct(branchProductId).price;
            return result;
        }
        public List<Product> GetRecommendedList(int consumerId)
        {
            var result = GetUsualShoppingsForEachDay(consumerId);
            if(result.ContainsKey(DateTime.Now.DayOfWeek.ToString()))
                return result[DateTime.Now.DayOfWeek.ToString()].Select(p => GetProductByName(p)).ToList();
            return null;
        }
        public IDictionary<string, List<string>> GetUsualShoppingsForEachDay(int consumerId, double minPrecent = 0.3)
        {
            return dal.GetUsualShoppingsForEachDay(consumerId, minPrecent);
        }
        public double SumOfTotalShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            IDictionary<string, double> ResultOfShoppingsBetweenTwoDates = ShoppingsBetweenTwoDatesByDay(start, end, consumerId);
            return ResultOfShoppingsBetweenTwoDates.Sum(x => x.Value);
        }
        public IDictionary<DateTime, double> GetShoppingsInBranchBetweenTwoDates(DateTime start, DateTime end, int consumerId, int BranchId)
        {
            return dal.GetShoppingsInBranchBetweenTwoDates(start, end, consumerId, BranchId);
        }
        public IDictionary<DateTime, double> GetShoppingsInCategoryBetweenTwoDates(DateTime start, DateTime end, int consumerId, int categoryName)
        {
            return dal.GetShoppingsInCategoryBetweenTwoDates(start, end, consumerId, categoryName);
        }
        #endregion
        #region GRAPH
        public Dictionary<string, double> CategoryBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, string categoryName)
        {
            return dal.CategoryBetweenTwoDatesByDay(start, end, consumerId, categoryName);
        }
        public Dictionary<string, double> CategoryBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, string categoryName)
        {
            return dal.CategoryBetweenTwoDatesByWeek(start, end, consumerId, categoryName);
        }
        public Dictionary<string, double> CategoryBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, string categoryName)
        {
            return dal.CategoryBetweenTwoDatesByMonth(start, end, consumerId, categoryName);
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int branchId)
        {
            return dal.BranchBetweenTwoDatesByDay(start, end, consumerId, branchId);
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int branchId)
        {
            return dal.BranchBetweenTwoDatesByWeek(start, end, consumerId, branchId);
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int branchId)
        {
            return dal.BranchBetweenTwoDatesByMonth(start, end, consumerId, branchId);
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int productId)
        {
            return dal.ProductBetweenTwoDatesByDay(start, end, consumerId, productId);
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int productId)
        {
            return dal.ProductBetweenTwoDatesByWeek(start, end, consumerId, productId);
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int productId)
        {
            return dal.ProductBetweenTwoDatesByMonth(start, end, consumerId, productId);
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId)
        {
            return dal.ShoppingsBetweenTwoDatesByDay(start, end, consumerId);
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId)
        {
            return dal.ShoppingsBetweenTwoDatesByWeek(start, end, consumerId);
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId)
        {
            return dal.ShoppingsBetweenTwoDatesByMonth(start, end, consumerId);
        }
        #endregion
        #region APRIORI
        public bool DoesProductExistsInList(List<OrderedProduct> ordered, int productId)
        {
            if (ordered.FindIndex(p => GetBranchProduct(p.branchProductId).productId == productId) == -1)
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Product> AprioriRecommender(List<OrderedProduct> orderedProducts, double minSupport, double minConfidence)
        {
            IApriori apriori = new Apriori();
            Output rules = apriori.ProcessTransaction(minSupport, minConfidence, GetProductsIdInList(), GetShoppingLists());
            List<Product> result = new List<Product>();
            bool Xexists = true;
            if(rules.StrongRules.Count() != 0)
            {
                rules.StrongRules = rules.StrongRules.OrderByDescending(r => r.Confidence).ToList();
                foreach (Rule rule in rules.StrongRules)
                {
                    Xexists = true;
                    List<int> combination = rule.X.Split(',').Select(x => int.Parse(x)).ToList();
                    foreach (int productId in combination)
                    {
                        if(DoesProductExistsInList(orderedProducts,productId) == false)
                        {
                            Xexists = false;
                            break;
                        }
                    }
                    if (Xexists)
                        foreach (string itemId in rule.Y.Split(',').ToList())
                        {
                            if(!result.Exists(p => p.id == int.Parse(itemId)) && !DoesProductExistsInList(orderedProducts, int.Parse(itemId)))
                                result.Add(GetProduct(int.Parse(itemId)));
                        }
                }
            }
            if(result.Count() == 0 && rules.FrequentItems.Count() != 0)
            {
                List<Item> FrequentItems = rules.FrequentItems.OrderByDescending(item => item.Support).ToList();
                foreach (Item item in FrequentItems)
                {
                    if (item.Name.Split(',').Count() == 1)
                    {
                        int itemId = int.Parse(item.Name.Split(',')[0]);
                        if (!DoesProductExistsInList(orderedProducts, itemId))
                            result.Add(GetProduct(itemId));
                    }
                }
            }
            return result;
        }

        public Dictionary<double, Dictionary<string, string>> ProductsBoughtTogether(int consumerId, double minSupport = 0.5, double minConfidence = 0.7)
        {
            IApriori apriori = new Apriori();
            Output rules = apriori.ProcessTransaction(minSupport, minConfidence, GetProductsIdInList(), GetShoppingLists());
            Dictionary<double, Dictionary<string, string>> result = new Dictionary<double, Dictionary<string, string>>();
            rules.StrongRules = rules.StrongRules.Where(k => k.X.Split(',').Count() == 1).ToList();
            foreach (Rule rule in rules.StrongRules)
            {
                if (!result.ContainsKey(rule.Confidence * 100))
                    result[rule.Confidence * 100] = new Dictionary<string, string>();
                string key = string.Join(", ", rule.X.Split(',').Select(i => GetProductName(int.Parse(i))).Distinct());
                string value = string.Join(", ", rule.Y.Split(',').Select(i => GetProductName(int.Parse(i))).Where(name => name != key).Distinct());
                if (!result[rule.Confidence * 100].ContainsKey(key))
                    result[rule.Confidence * 100][key] = value;

            }
            return result;
        }
        public IDictionary<List<Product>, double> ProductsThatGoTogether(double minConfidence = 0.01)
        {
            return dal.ProductsThatGoTogether(minConfidence);
        }
        #endregion
       
    }
}
