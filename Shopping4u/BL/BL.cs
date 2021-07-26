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
        #region FIREBASE
        public async Task StorePicture(string uploadUrl, string name, int productId)
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
            UpdateProductPicture(downloadUrl, productId);
        }
        public void UpdateProductPicture(string downloadUrl, int productId)
        {
            dal.UpdateProductPicture(downloadUrl, productId);
        }
        public async Task StoreBarcode(string uploadUrl, string name, int shoppingListId)
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
            EncodeBarcode(downloadUrl,shoppingListId);
        }
        // encodes the barcodes
        public void EncodeBarcode(string downloadUrl, int shoppingListId)
        {
            string imageUrl = downloadUrl;
            // Install-Package ZXing.Net -Version 0.16.5
            var client = new WebClient();
            var stream = client.OpenRead(imageUrl);
            if (stream == null) return;
            var bitmap = new Bitmap(stream);
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            // id, nameOfBranch, nameOfProduct, price
            string[] barcodeText = result.Text.Split(',');
            Branch branch = new Branch { 
                name = barcodeText[1]
            };
            Product product = new Product
            {
                id = int.Parse(barcodeText[0]),
                name = barcodeText[2]
            };
            double price = double.Parse(barcodeText[3]);
            BranchProduct branchProduct = dal.InsertBranchProduct(product, branch, price);

            OrderedProduct orderedProduct = new OrderedProduct {
                branchProductId = branchProduct.branchProductId,
                shoppingListId = shoppingListId
            };
            dal.InsertOrderedProduct(orderedProduct, shoppingListId);
        }
        #endregion
    }
}
