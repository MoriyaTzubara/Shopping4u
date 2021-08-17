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
    public class CategoriesChartModel : ILineChartModel<string,string>
    {
        public Dictionary<string, double> getData(string categoryName, AggregateBy aggregateBy, DateTime startDate, DateTime endDate)
        {
            // TODO //
            IBL bl = new BL.BL();
            switch (aggregateBy)
            {
                case AggregateBy.MONTH:
                    return bl.CategoryBetweenTwoDatesByMonth(startDate.AddYears(-1), startDate, 1, categoryName);
                case AggregateBy.WEEK:
                    return bl.CategoryBetweenTwoDatesByWeek(startDate.AddMonths(-1), startDate, 1, categoryName);
                case AggregateBy.DAY:
                    return bl.CategoryBetweenTwoDatesByDay(startDate.AddDays(-7), startDate, 1, categoryName);
                default:
                    break;
            }
            return new Dictionary<string, double>();
        }

        public IEnumerable<string> getOption()
        {
            // TODO //
            IBL bl = new BL.BL();
            return bl.GetCategoriesNames();
        }
    }
}