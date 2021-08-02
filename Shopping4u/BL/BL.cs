using BE;
using BL;
using BL.Contracts;
using BL.Entities;
using Firebase.Storage;
using Shopping4u.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (dal.GetConsumer(consumer.id) != new Consumer())
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
        public bool SignIn(int consumerId,string password)
        {
            Consumer consumer = GetConsumer(consumerId);
            if (consumer.password != password)
                return false;
            return true;
        }
        #endregion
        #region FIREBASE
        public async Task<string> StorePicture(string uploadUrl, string name)
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
            //Console.WriteLine(downloadUrl);
            return downloadUrl;
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
            return result.Text;
        }
        #endregion
        #region SELECT
        public List<Product> GetProducts()
        {
            return dal.GetProducts();
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
        public List<string> GetBranchesNameOfSpecificProduct(int productId)
        {
            return dal.GetBranchesNameOfSpecificProduct(productId);
        }
        public List<string> GetProductsNameOfSpecificBranch(int branchId)
        {
            return dal.GetProductsNameOfSpecificBranch(branchId);
        }
        public List<string> GetCategoriesNames()
        {
            return dal.GetCategoriesNames();
        }
        #endregion
        #region INSERT  
        public void InsertShoppingList(ShoppingList shoppingList)
        {
            dal.InsertShoppingList(shoppingList);
        }

        public void InsertOrderedProducts(List<OrderedProduct> orderedProducts, int shoppingListId)
        {
            dal.InsertOrderedProducts(orderedProducts, shoppingListId);
        }

        public void InsertOrderedProduct(string orderedProductText, int shoppingListId)
        {
            // id, nameOfBranch, nameOfProduct, price
            string[] barcodeText = orderedProductText.Split(',');
            Branch branch = new Branch
            {
                name = barcodeText[1]
            };
            Product product = new Product
            {
                id = int.Parse(barcodeText[0]),
                name = barcodeText[2]
            };
            double price = double.Parse(barcodeText[3]);
            BranchProduct branchProduct = InsertBranchProduct(product, branch, price);

            OrderedProduct orderedProduct = new OrderedProduct
            {
                branchProductId = branchProduct.branchProductId,
                shoppingListId = shoppingListId
            };
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

        public void InsertConsumer(Consumer consumer)
        {
            dal.InsertConsumer(consumer);
        }
        public void InsertOrderedProduct(OrderedProduct orderedProduct)
        {
            dal.InsertOrderedProduct(orderedProduct);
        }
        #endregion
        #region UPDATE
        public void UpdateOrderedProduct(int quantity, int shoppingListId, int branchProductId)
        {
            dal.UpdateOrderedProduct(quantity, shoppingListId, branchProductId);
        }
        public void UpdateProductPicture(string downloadUrl, int productId)
        {
            dal.UpdateProductPicture(downloadUrl, productId);
        }
        #endregion
        #region DELETE
        public void DeleteOrderedProduct(int shoppingListId, int branchProductId)
        {
            dal.DeleteOrderedProduct(shoppingListId, branchProductId);
        }
        #endregion
        #region FILTERS
        public Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            return dal.OrderedProductsBetweenTwoDates(start, end, consumerId);
        }

        public Dictionary<DateTime, double> ShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            return dal.ShoppingsBetweenTwoDates(start, end, consumerId);
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
        public IDictionary<string, List<string>> GetUsualShoppingsForEachDay(int consumerId, double minPrecent = 0.3)
        {
            return dal.GetUsualShoppingsForEachDay(consumerId, minPrecent);
        }
        public double SumOfTotalShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            IDictionary<DateTime, double> ResultOfShoppingsBetweenTwoDates = ShoppingsBetweenTwoDates(start, end, consumerId);
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
        #region APRIORI
        public bool DoesProductExistsInList(List<OrderedProduct> ordered, int productId)
        {
            if (ordered.FindIndex(p => GetBranchProduct(p.branchProductId).productId == productId) == -1)
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Product> AprioriRecommender(List<OrderedProduct> orderedProducts, double minSupport = 0.01, double minConfidence=0.01)
        {
            IApriori apriori = new Apriori();
            Output rules = apriori.ProcessTransaction(minSupport, minConfidence, GetProductsIdInList(), GetShoppingLists());
            List<Product> result = new List<Product>();
            bool Xexists = true;
            if(rules.StrongRules.Count() != 0)
            {
                foreach (Rule rule in rules.StrongRules)
                {
                    List<int> combination = rule.X.Split(',').Select(x => int.Parse(x)).ToList();
                    foreach (int productId in combination)
                    {
                        if(DoesProductExistsInList(orderedProducts,productId) == false)
                        {
                            Xexists = false;
                            break;
                        }
                    }
                    if (Xexists && !result.Exists(p => p.id == int.Parse(rule.Y)))
                        result.Add(GetProduct(int.Parse(rule.Y)));
                }
            }
            else if(rules.FrequentItems.Count() != 0)
            {
                foreach (Item item in rules.FrequentItems)
                {
                    int productId = int.Parse(item.Name);
                    if(DoesProductExistsInList(orderedProducts, productId) == false)
                    {
                        result.Add(GetProduct(productId));
                    }
                }
            }
            return result;
        }
        public List<List<Product>> ProductsBoughtTogether(int consumerId, double minSupport = 0.01, double minConfidence = 0.01)
        {
            List<List<Product>> result = new List<List<Product>>();
            IApriori apriori = new Apriori();
            Output rules = apriori.ProcessTransaction(minSupport, minConfidence, GetProductsIdInList(), GetShoppingListsOfConsumer(consumerId));
            List<Product> ProductsTogether;
            if (rules.ClosedItemSets.Count() != 0)
            {
                foreach (KeyValuePair<string,Dictionary<string,double>> closedItems in rules.ClosedItemSets)
                {
                    List<List<int>> ListOfClosedItems = closedItems.Value.Keys.ToList().Select(k => k.Split(',').Select(x => int.Parse(x)).ToList()).ToList();
                    
                    foreach (List<int> listOfProductsId in ListOfClosedItems)
                    {
                        ProductsTogether = new List<Product>();
                        foreach (int productId in listOfProductsId)
                        {
                            ProductsTogether.Add(GetProduct(productId));
                        }
                        result.Add(ProductsTogether);
                    }
                    
                }
            }
            return result;
        }
        public IDictionary<List<Product>, double> ProductsThatGoTogether(double minConfidence = 0.01)
        {
            return dal.ProductsThatGoTogether(minConfidence);
        }
        #endregion
        //#region APRIORI
        //public IEnumerable<BE.Rule> AprioriRecommender(double minConfidence = 0.3,double minSupport = 0.2)
        //{
        //    IDictionary<int, int> forEachProduct = dal.GetSupportOfEachItem(minSupport);
        //    List<BE.Rule> Rules = dal.AprioriRecommender(forEachProduct,minConfidence);
        //    return Rules;
        //}
        //#endregion

    }
}
