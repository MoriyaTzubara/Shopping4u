using System;
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
    /// Interaction logic for RecommendtionUserControl1.xaml
    /// </summary>
    public partial class RecommendtionUserControl : UserControl
    {
        public RecommendtionUserControl(RecommendtionViewModel recommendtionViewModel)
        {
            InitializeComponent();
            DataContext = recommendtionViewModel;
        }

    }
}
