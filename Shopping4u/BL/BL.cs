using BE;
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

        public List<Product> GetProductsByName(string name)
        {
            return dal.GetProductsByName(name);
        }

        public List<OrderedProduct> FilterByBranches(List<string> branchesNames, int shoppingListId)
        {
            return dal.FilterByBranches(branchesNames, shoppingListId);
        }
        #endregion
        #region APRIORI

        #endregion
    }
}
