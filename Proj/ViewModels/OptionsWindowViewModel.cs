using Proj.Commands;
using Proj.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Proj.ViewModels
{
    class OptionsWindowViewModel : ViewModelBase
    {

        private List<CultureInfo> languagesList = null;
        public List<CultureInfo> LanguageList
        {
            get 
            {
                if (languagesList == null)
                {
                    languagesList = new List<CultureInfo>();

                    ResourceManager resourceManager = new ResourceManager(typeof(Loc.Lang));
                    CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                    foreach (CultureInfo c in allCultures)
                    {
                        ResourceSet resourceSet = resourceManager.GetResourceSet(c, true, false);
                        if (resourceSet != null && c.LCID != 127)
                        {
                            if (c.Parent.Equals(CultureInfo.InvariantCulture))
                                languagesList.Add(c);
                            else
                                languagesList.Add(c.Parent);
                        }
                    }
                }

                return this.languagesList;
            }
        }

        public CultureInfo Language
        {
            get 
            {

                if (Settings.Default.Language.Equals(CultureInfo.InvariantCulture))
                {
                    if (LanguageList.Contains(Thread.CurrentThread.CurrentUICulture.Parent)) Settings.Default.Language = Thread.CurrentThread.CurrentUICulture.Parent;
                    else Settings.Default.Language = new CultureInfo("en");

                    return Settings.Default.Language;
                }

                else return Settings.Default.Language;

                
            }
            set 
            { 
                Settings.Default.Language = value;
                OnPropertyChanged("Language");
            }
        }

        public void Save(object ignore)
        {
            Settings.Default.Save();
            
        }

        #region Commands

        private RelayCommand saveOptionsCommand;
        public ICommand SaveOptionsCommand
        {
            get
            {
                if (this.saveOptionsCommand == null)
                {
                    this.saveOptionsCommand = new RelayCommand(this.Save);
                }
                return this.saveOptionsCommand;
            }
        }

        #endregion
    }
}
