using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Proj.Services;
using System.Threading;
using System.Globalization;
using Proj.ViewModels;
using Proj.Views;
using Proj.Properties;
using System.Resources;

namespace Proj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessengerService.ShowMessageBox(e.Exception.Message, "Błąd", MessengerService.MessageType.OK, MessengerService.MessageIcon.Error);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            //TODO Do testow lokalizacji
            string language = Settings.Default.Language.ToString();
            if (String.IsNullOrEmpty(language))
            {
                Settings.Default.Language = Thread.CurrentThread.CurrentUICulture;
                Settings.Default.Save();
            }
            else
                Proj.Loc.Lang.Culture = new System.Globalization.CultureInfo(language);

            OptionsWindowView options = new OptionsWindowView();
            options.Show();
        }

    }    
}
