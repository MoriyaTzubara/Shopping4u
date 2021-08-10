﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shopping4u.ViewModels;

namespace Shopping4u.Views
{
    /// <summary>
    /// Interaction logic for StatisticUserControl.xaml
    /// </summary>
    public partial class LineChartUserControl : UserControl
    {
        public LineChartUserControl(ChartViewModel<double> statisticViewModel)
        {
            InitializeComponent();
            DataContext = statisticViewModel;
        }
    }
}
