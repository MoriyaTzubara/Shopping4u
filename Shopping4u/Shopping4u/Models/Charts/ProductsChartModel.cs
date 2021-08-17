﻿using BE;
using Shopping4u.BL;
using Shopping4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models.Charts
{
    public class ProductsChartModel : ILineChartModel<Product,int>
    {
        public Dictionary<string, double> getData(int productId, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.ProductBetweenTwoDatesByMonth(startDate.AddYears(-1), startDate, 1, productId);
                case AggregateBy.WEEK:
                    return bl.ProductBetweenTwoDatesByWeek(startDate.AddMonths(-1), startDate, 1, productId);
                case AggregateBy.DAY:
                    return bl.ProductBetweenTwoDatesByDay(startDate.AddDays(-7), startDate, 1, productId);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }

        public IEnumerable<Product> getOption()
        {
            // TODO //
            IBL bl = new BL.BL();
            return bl.GetProducts();
        }
    }
}