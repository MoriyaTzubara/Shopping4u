﻿using BE;
using LiveCharts;
using LiveCharts.Wpf;
using Shopping4u.BL;
using Shopping4u.Commands;
using Shopping4u.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.ViewModels.Charts
{
    public class CategoriesChartViewModel : ILineChartViewModel
    {
        private CategoriesChartModel CategorysChartModel;
        public CategoriesChartViewModel()
        {
            CategorysChartModel = new CategoriesChartModel();
            Options = getOption();
            CurrentCategory = Options.ElementAtOrDefault(0) as string;

            Data = CategorysChartModel.getData(CurrentCategory, AggregateBy.WEEK, DateTime.Now);
            setSeriesCollection(Data);

            SelectOptionCommand = new SelectOptionCommand(this);
        }

        public Dictionary<string, double> Data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public IEnumerable<object> Options { get; set; }
        public string CurrentCategory { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AggregateBy AggregateBy { get; set; }

        public SelectOptionCommand SelectOptionCommand { get; set; }


        public void selectOption(object option)
        {

        }
        public void selectDates(DateTime start, DateTime end)
        {

        }


        public Dictionary<string, double> getData(string CategoryName, AggregateBy aggregateBy, DateTime endDate)
        {
            // TODO //
            return CategorysChartModel.getData(CategoryName, aggregateBy, endDate);
        }

        public IEnumerable<string> getOption()
        {
            // TODO //
            return CategorysChartModel.getOption();

        }

        public void setSeriesCollection(Dictionary<string, double> data)
        {
            // TODO //

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(data.Values),
                }
            };
        }
    }
}
