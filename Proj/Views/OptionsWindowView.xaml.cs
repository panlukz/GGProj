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
using System.Windows.Shapes;

using Proj.ViewModels;

namespace Proj.Views
{
    /// <summary>
    /// Interaction logic for OptionsWindowView.xaml
    /// </summary>
    public partial class OptionsWindowView : Window
    {
        public OptionsWindowView()
        {
            this.DataContext = new OptionsWindowViewModel();
            InitializeComponent();
        }

        //TODO jedyny code-behind do zamykania okienka:)
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
