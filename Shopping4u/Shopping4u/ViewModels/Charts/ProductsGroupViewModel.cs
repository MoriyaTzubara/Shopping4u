using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shopping4u.Models.Charts;

namespace Shopping4u.ViewModels.Charts
{
    public class ProductsGroupViewModel : INotifyPropertyChanged
    {
        #region PROPERTIRES
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        ProductsGroupModel productsGroupModel;


        private string showListView;
        public string ShowListView
        {
            get { return showListView; }
            set { showListView = value; OnPropertyChanged(); }
        }

        private List<ProductsGroup> productsGroups;
        public List<ProductsGroup> ProductsGroups 
        {
            get { return productsGroups; }
            set { productsGroups = value; OnPropertyChanged(); }
        }
        #endregion
        #region CONSTRUCTOR
        public ProductsGroupViewModel()
        {
            productsGroupModel = new ProductsGroupModel();

            productsGroupModel.ProductsBoughtTogetherEvent += ProductsBoughtTogetherHandle;

            productsGroupModel.getProductsGroup();
            ShowListView = "Collapsed";
        }
        #endregion
        #region EVENTS
        private void ProductsBoughtTogetherHandle(object sender, Dictionary<double, Dictionary<string, string>> productGroups)
        {
            var groups = productGroups;

            ProductsGroups = new List<ProductsGroup>();
            foreach(var group in groups)
            {
                ProductsGroups.Add(new ProductsGroup($"{group.Key}%", string.Join("\n", group.Value.Select(x => $"* {x.Key} -> {x.Value}"))));
            }
            ShowListView = "Visible";
        }
        #endregion
    }

    public class ProductsGroup
    {
        #region CONSTRUCTOR
        public ProductsGroup()
        {
            Probability = "";
            Groups = "";
        }
        public ProductsGroup(string probability, string groups)
        {
            Probability = probability;
            Groups = groups;

        }
        #endregion
        #region PROPERTIRES
        public string Probability { get; set; }
        public string Groups { get; set; }
        #endregion
    }

}
