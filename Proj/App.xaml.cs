using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Proj.Services;

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
    }

    
}
