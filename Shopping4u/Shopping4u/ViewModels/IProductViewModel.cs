﻿using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels
{
    public interface IProductViewModel
    {
        string ImgUrl { get; set; }
        int Quantity { get; set; }
        string UnitPrice { get; set; }
    }
}
