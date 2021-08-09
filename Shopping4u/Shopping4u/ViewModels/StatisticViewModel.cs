﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shopping4u.Models;
using System.Threading.Tasks;
using LiveCharts.Wpf;

namespace Shopping4u.ViewModels
{
    public class StatisticViewModel<T>
    {
        public string Title { get; set; }
        public IEnumerable<T> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public StatisticViewModel(StatisticModel<T> statisticModel)
        {
            Title = statisticModel.Title;
            Data = statisticModel.Data;
        }
    }
}
