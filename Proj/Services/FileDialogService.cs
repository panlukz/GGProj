using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Services
{
    static class FileDialogService
    {
        public static string ShowOpenFileDialog()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.FileName = "";
            openDlg.DefaultExt = ".xml";
            openDlg.Filter = "Xml documents (.xml)|*.xml";

            Nullable<bool> dialogResult = openDlg.ShowDialog();

            return openDlg.FileName;
        }

        public static string ShowSaveAsFileDialog()
        {
            SaveFileDialog saveAsDlg = new SaveFileDialog();
            saveAsDlg.FileName = "";
            saveAsDlg.DefaultExt = ".xml";
            saveAsDlg.Filter = "Xml documents (.xml)|*.xml";


            Nullable<bool> result = saveAsDlg.ShowDialog();

            return saveAsDlg.FileName;
        }
    }
}
