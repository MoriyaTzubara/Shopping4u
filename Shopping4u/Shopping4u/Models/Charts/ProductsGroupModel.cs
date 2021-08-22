using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class ProductsGroupModel
    {
        public event EventHandler<Dictionary<double, Dictionary<string, string>>> ProductsBoughtTogetherEvent;

        public async void getProductsGroup() 
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                IBL bl = new BL.BL();
                Dictionary <double, Dictionary<string, string>> result = bl.ProductsBoughtTogether(App.Consumer.id);
                ProductsBoughtTogetherEvent(this, result);

            }).Start();
        }

    }
}
