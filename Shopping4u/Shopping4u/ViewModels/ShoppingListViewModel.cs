using BE;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models;
using System.Windows;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Drawing.Layout;

namespace Shopping4u.ViewModels
{
    public abstract class ShoppingListViewModel : INotifyPropertyChanged
    {

        public ShoppingListModel ShoppingListModel;


        public ShoppingListViewModel(ShoppingListModel shoppingListModel)
        {
            ShoppingListModel = shoppingListModel;

            Products = new ObservableCollection<OrderedProductViewModel>(shoppingListModel.Products.Select(x => new OrderedProductViewModel(x)));
            
            CreateProductCommand = new CreateProductCommand(this);
            UpdateProductCommand = new UpdateProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);
            ExportRecommendedListToPDFCommand = new ExportRecommendedListToPDFCommand(this);

            ShowCreateProductCommand = new ShowCreateProductCommand(this);

            ProductConverter = new ProductConverter();
            IsShowCreateProduct = false;

            numberOfProducts = Products.Count;
            TotalPrice = calculateTotalPrice();
            shoppingListId = shoppingListModel.shoppingListId;
        }


        private string format(object obj, int len=30)
        {
            string str = obj.ToString();
            return str + string.Concat(Enumerable.Repeat(" ", len - str.Length));
        }
        internal void ExportRecommendedListToPDF()
        {

            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont fontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont fontHeader = new XFont("Verdana", 18, XFontStyle.Bold);

            List<string> productsAsString = Products.Select(x => $"   {format(x.ProductName)}{format(x.BranchName)}{format(x.Quantity)}{format(x.UnitPrice+"$")}").ToList();

            graph.DrawString("Recommended Shopping List", fontHeader, XBrushes.Purple, 30, 70);
            graph.DrawString($"{format("Product Name", 25)}{format("Branch Name", 25)}{format("Quantity", 25)}{format("UnitPrice", 25)}", fontBold, XBrushes.Black, 40, 150);

            int i = 1;
            graph.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.Purple)), 0, 100 + 40 * i + 20, 1000, 100 + 40 * i + 20);

            foreach (var item in productsAsString)
            {
                i += 1;
                graph.DrawString(item, font, XBrushes.Black, 30, 100 + 40*i);
                graph.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.Purple)), 0, 100 + 40 * i + 20, 1000, 100 + 40 * i + 20);
            }
            IBL bl = new BL.BL();
            graph.DrawLine(new XPen(XColor.FromKnownColor(XKnownColor.Purple)), 0, 100 + 40 * i + 20, 1000, 100 + 40 * i + 20);
            graph.DrawString($"Total: {totalPrice}$", fontBold, XBrushes.Black, 40, 100 + 40 * i + 100);

            string filename = "HelloWorld.pdf";
            pdf.Save(filename);

            Process.Start(filename);
        }


        public ShowCreateProductCommand ShowCreateProductCommand { get; set; }
        public CreateProductCommand CreateProductCommand { get; set; }
        public UpdateProductCommand UpdateProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }
        public ExportRecommendedListToPDFCommand ExportRecommendedListToPDFCommand { get; set; }


        public ProductConverter ProductConverter { get; set; }

        public void ShowCreateProduct(bool isShow)
        {
            IsShowCreateProduct = isShow;
        }


        public CreateProductViewModel CreateProductViewModel { get; set; }

        private bool isShowCreateProduct;
        public bool IsShowCreateProduct {
            get { return isShowCreateProduct; }
            set
            {
                isShowCreateProduct = value;
                CreateProductVisibility = value ? "Visible" : "Collapsed";
                OnPropertyChanged();
            }
        }

        private string isShowSaveList;
        public string IsShowSaveList
        {
            get { return isShowSaveList; }
            set
            {
                isShowSaveList = value;
                OnPropertyChanged();
            }
        }

        private string createProductVisibility;
        public string CreateProductVisibility
        {
            get { return createProductVisibility; }
            set {
                createProductVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string Title { get; set; }
        public int shoppingListId;

        private ObservableCollection<OrderedProductViewModel> products;
        public ObservableCollection<OrderedProductViewModel> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<OrderedProductViewModel> GetProducts() {
            return ShoppingListModel.Products.Select(x => new OrderedProductViewModel(x));
        }
        
        public virtual void CreateProduct(OrderedProduct orderedProduct)
        {
            int orderedProductIndex = products.ToList().FindIndex(o => o.BranchProductId == orderedProduct.branchProductId);
            if (orderedProductIndex != -1)
            {
                products[orderedProductIndex].orderedProduct.quantity += orderedProduct.quantity;
                products[orderedProductIndex].Quantity += orderedProduct.quantity;
            }
            else
            {
                var newProducts = new List<OrderedProductViewModel>();
                newProducts.Add(new OrderedProductViewModel(orderedProduct));
                newProducts.AddRange(Products.ToList());

                Products = new ObservableCollection<OrderedProductViewModel>(newProducts);
            }
            NumberOfProducts = products.Count();
            TotalPrice = calculateTotalPrice();
        }
        public virtual void DeleteProduct(int productId)
        {
            OrderedProductViewModel product = Products.First(x => x.Id == productId);
            Products.Remove(product);

            NumberOfProducts -= 1;
            TotalPrice -= product.orderedProduct.unitPrice * product.Quantity;
        }
        public virtual void UpdateProduct(OrderedProduct orderedProduct)
        {
            int index = Products.IndexOf(Products.First(x => x.Id == orderedProduct.id));
            Products[index] = new OrderedProductViewModel(orderedProduct);

            TotalPrice = calculateTotalPrice();
            ShoppingListModel.UpdateProduct(orderedProduct);
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int numberOfProducts;
        public int NumberOfProducts
        {
            get { return numberOfProducts; }
            set 
            {
                numberOfProducts = value;
                OnPropertyChanged();
            }
        }


        private double totalPrice { get; set; }
        public double  TotalPrice 
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }
        private double calculateTotalPrice()
        {
            return Products.Sum(x => x.orderedProduct.unitPrice * x.Quantity);
        }

    }
}
