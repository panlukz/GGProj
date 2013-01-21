using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

using Proj.Model;
using Proj.ViewModels;

namespace Proj.Views
{
    /// <summary>
    /// Interaction logic for ListWindowView.xaml
    /// </summary>
    public partial class ListWindowView : Window
    {

        public ListWindowViewModel ViewModel;

        public ListWindowView(ListWindowViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;

            InitializeComponent();

            //////////////////////////////////

            foreach (DataGridColumn column in Data.Columns)
            {
                if (!ViewModel.ListColumns.Contains(column.Header.ToString()))
                {
                    column.Visibility = Visibility.Hidden;
                }
            }

        }
    }
}
