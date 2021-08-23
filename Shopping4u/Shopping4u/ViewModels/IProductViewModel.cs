using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public interface IProductViewModel
    {
        #region PROPERTIRES
        string ImgUrl { get; set; }
        int Quantity { get; set; }
        double UnitPrice { get; set; }
        #endregion
    }
}
