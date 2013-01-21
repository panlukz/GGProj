using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Deployment;
using System.Deployment.Application;


namespace Proj.Views
{
    /// <summary>
    /// Interaction logic for AboutWindowView.xaml
    /// </summary>
    public partial class AboutWindowView : Window
    {

        public string GetAppVersion()
        {
            string version;

            try
            {
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch(InvalidDeploymentException e)
            {
                version = e.Message.ToString();
            }

            return version;
        }

        public AboutWindowView()
        {
            InitializeComponent();

            versionTextBlock.Text = "Wersja aplikacji: " + GetAppVersion();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
