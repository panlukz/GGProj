using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proj.Model;
using Proj.Services;
using Proj.Commands;
using Proj.Data;
using Proj.Views;

using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace Proj.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {

        #region Constructor

        public MainWindowViewModel()
            : base()
        {

            ProductList = new ProductsCollection<Product>();
           
        }

        #endregion

        #region Members
        private ProcessProductsListService processProductsListService = new ProcessProductsListService();

        private DataService dataService = new DataService();
        private ProductsCollection<Product> productList;
        private Product selectedProduct;

        private bool allSelected;

        private bool isSaved;
        private bool isBusy;
        private bool isListOk;

        private string filePath = string.Empty;
        
        private int selectedProductIndex;
       
        #endregion

        #region Properties

        public ProductsCollection<Product> ProductList
        {
            get
            {
                return this.productList;
            }

            set
            {
                this.productList = value;
                OnPropertyChanged("ProductList");
                OnPropertyChanged("ProductCount");
            }
        }

        public Product SelectedProduct
        {
            get
            {
                return this.selectedProduct;
            }

            set
            {
                this.selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        
        public int SelectedProductIndex
        {
            get
            {
                if (selectedProductIndex == 0)
                {
                    if (ProductList != null && SelectedProduct != null)
                    {
                        selectedProductIndex = ProductList.IndexOf(SelectedProduct);
                        return selectedProductIndex;
                    }
                    else
                        return -1;
                }
                else
                    return selectedProductIndex;
            }
            set
            {
                this.selectedProductIndex = value;
                OnPropertyChanged("SelectedProductIndex");
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public bool IsListOk
        {
            get
            {
                return isListOk;
            }
            set
            {
                isListOk = value;
            }

        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                this.filePath = value;
                OnPropertyChanged("FilePath");
                OnPropertyChanged("AppTitle");
            }
        }

        public string FileName
        {
            get
            {
                if (!String.IsNullOrEmpty(FilePath))
                {
                    FileInfo fileInfo = new FileInfo(FilePath);
                    return fileInfo.Name;
                }
                else
                    return String.Empty;
                
            }
        }

        public bool IsSaved
        {
            get
            {
                return isSaved;
            }
            set
            {
                isSaved = value;
                OnPropertyChanged("IsSaved");
            }
        }

        public string AppTitle
        {
            get
            {
                string appName = Loc.Lang.AppName;
                if (String.IsNullOrEmpty(FileName))
                    return appName + " - Nowy plik";
                else
                    return appName + " - " + FileName;
            }
        }

        public bool AllSelected
        {
            get
            {
                return allSelected;                
            }

            set
            {
                allSelected = value;
                OnPropertyChanged("AllSelected");
                if (ProductList != null)
                {
                    foreach (Product product in ProductList)
                    {
                        product.CzyKupowac = value;
                    }
                }

                
            }
        
        }

        public int ProductsToBuyCount
        {
            get
            {
                int productsToBuy = 0;

                foreach (Product product in ProductList)
                {
                    if (product.CzyKupowac == true) productsToBuy++;
                }
                return productsToBuy;
            }

            set
            { 
                
            }
        
        }

        #endregion

        #region Commands

        private RelayCommand aboutWindowCommand;
        public ICommand AboutWindowCommand
        {
            get
            {
                if (this.aboutWindowCommand == null)
                {
                    this.aboutWindowCommand = new RelayCommand(this.ShowAboutWindow);
                }
                return this.aboutWindowCommand;
            }
        }

        private RelayCommand listWindowCommand;
        public ICommand ListWindowCommand
        {
            get
            {
                if (this.listWindowCommand == null)
                {
                    this.listWindowCommand = new RelayCommand(param => this.ShowListWindow(param), this.CanShowListWindowCommand);
                }
                return this.listWindowCommand;
            }
        }

        private RelayCommand openFileCommand;
        public ICommand OpenFileCommand
        {
            get
            {
                if (this.openFileCommand == null)
                {
                    this.openFileCommand = new RelayCommand(this.OpenFile);
                }
                return this.openFileCommand;
            }
        }

        private RelayCommand saveFileCommand;
        public ICommand SaveFileCommand
        {
            get
            {
                if (this.saveFileCommand == null)
                {
                    this.saveFileCommand = new RelayCommand(this.SaveFile);
                }
                return this.saveFileCommand;
            }
        }

        private RelayCommand saveAsFileCommand;
        public ICommand SaveAsFileCommand
        {
            get
            {
                if (this.saveAsFileCommand == null)
                {
                    this.saveAsFileCommand = new RelayCommand(this.SaveAsFile);
                }
                return this.saveAsFileCommand;
            }
        }

        private RelayCommand newFileCommand;
        public ICommand NewFileCommand
        {
            get
            {
                if (this.newFileCommand == null)
                {
                    this.newFileCommand = new RelayCommand(this.NewFile);
                }
                return this.newFileCommand;
            }
        }

        private RelayCommand addLineCommand;
        public ICommand AddLineCommand
        {
            get
            {
                if (this.addLineCommand == null)
                {
                    this.addLineCommand = new RelayCommand(this.AddLine);
                }
                return this.addLineCommand;
            }
        }

        private RelayCommand removeLineCommand;
        public ICommand RemoveLineCommand
        {
            get
            {
                if (this.removeLineCommand == null)
                {
                    this.removeLineCommand = new RelayCommand(this.RemoveLine, this.CanRemoveCommand);
                }
                return this.removeLineCommand;
            }
        }

        private RelayCommand moveCommand;
        public ICommand MoveCommand
        {
            get
            {
                if (this.moveCommand == null)
                {
                    this.moveCommand = new RelayCommand(this.Move, this.CanMoveCommand);
                }
                return this.moveCommand;
            }
        }

        private RelayCommand sortListCommand;
        public ICommand SortListCommand
        {
            get
            {
                if (this.sortListCommand == null)
                {
                    this.sortListCommand = new RelayCommand(this.SortList, this.CanSortListCommand);
                }
                return this.sortListCommand;
            }
        }

        private RelayCommand closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                if (this.closeAppCommand == null)
                {
                    this.closeAppCommand = new RelayCommand(this.CloseApp);
                }
                return this.closeAppCommand;
            }
        }

        #endregion

        #region Helper Methods

        private void ShowAboutWindow(object ignore)
        {
            AboutWindowView about = new AboutWindowView();
            about.Owner = Application.Current.MainWindow;
            about.ShowDialog();
        }

        private void ShowListWindow(object list)
        {
            IEnumerable<Product> showList = null;
            string listType = list.ToString();
            
            switch (listType)
            {
                case "shoppingList":
                    showList = processProductsListService.GenerateShoppingList(ProductList);
                    break;
                case "sellList":
                    showList = processProductsListService.GenerateSellList(ProductList);
                    break;
                default:
                    break;
            }

            ListWindowViewModel listWindowViewModel = new ListWindowViewModel(listType) { ProductList = showList  };
            ListWindowView listWindow = new ListWindowView(listWindowViewModel);
            
            listWindow.Owner = Application.Current.MainWindow;
            listWindow.ShowDialog();
        }

        private void OpenFile(object ignore)
        {

            string filePath = FileDialogService.ShowOpenFileDialog();
            ProductsCollection<Product> tmpProductsList = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                //TODO Zmien to na TaskFactory
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;

                bw.DoWork += delegate(object sender, DoWorkEventArgs e)
                             {
                                 this.IsBusy = true;
                                 BackgroundWorker worker = sender as BackgroundWorker;

                                 for (int i = 0; i < 1; i++)
                                 {
                                     worker.ReportProgress(i);
                                     //Thread.Sleep(5000);  //TODO Do testów
                                 }
                                 tmpProductsList = dataService.OpenCollectionFromFile(filePath) as ProductsCollection<Product>;
                             };

                bw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                                         {
                                             this.IsBusy = false;
                                             this.ProductList = tmpProductsList;
                                             this.FilePath = filePath;
                                             this.IsSaved = true;
                                         };

                if(bw.IsBusy == false) bw.RunWorkerAsync();
            }
        }

        private void SaveAsFile(object ignore = null)
        {
            string filePath = FileDialogService.ShowSaveAsFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                dataService.SaveCollectionToFile(ProductList, filePath);
                this.FilePath = filePath;
                this.IsSaved = true;
            }
        }

        private void SaveFile(object ignore)
        {
            if (String.IsNullOrEmpty(this.FilePath))
            {
                SaveAsFile();
            }
            else
            {
                dataService.SaveCollectionToFile(ProductList, this.FilePath);
                this.IsSaved = true;
            }
        }

        private void NewFile(object ignore)
        {
            ProductList = new ProductsCollection<Product>();
            this.FilePath = null;
        }

        private void AddLine(object param)
        {   
            Product NewProduct = new Product {
                    Nazwa = "Nowy produkt",
                    CenaZakupu = 1,
                    CenaSprzedazy = 1,
                    MinIloscSprzedazy = 1,
                    MaxIloscSprzedazy = 1,
                    Popularnosc = 1, 
            };

            if (SelectedProduct == null)
            {
                ProductList.Add(NewProduct);
            }
            else
            {
                ProductList.Insert(ProductList.IndexOf(SelectedProduct)+1, NewProduct);
                SelectedProductIndex += 1;
            }       
        }

        private void RemoveLine(object param)
        {
            string productName = (String.IsNullOrEmpty(SelectedProduct.Nazwa) || String.IsNullOrWhiteSpace(SelectedProduct.Nazwa))
                                 ? "bez nazwy"
                                 : SelectedProduct.Nazwa;
            MessageBoxResult result = MessageBox.Show("Usunąć produkt: " + productName, "Potwierdzenie usunięcia linii", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) ProductList.Remove(SelectedProduct);      
        }

        private void Move(object param)
        {
            int direction = 0;

            switch (param.ToString())
            {
                case "Up":
                    direction = -1;
                    break;

                case "Down":
                    direction = 1;
                    break;
            }

            if ((ProductList.IndexOf(SelectedProduct) == 0 && direction == -1) || (ProductList.IndexOf(SelectedProduct) == ProductList.Count - 1 && direction == 1))
            {
                //do nothing because it's out of range
            }
            else
            {
                ProductList.Move(ProductList.IndexOf(SelectedProduct), ProductList.IndexOf(SelectedProduct) + direction);
            }
        }

        private void CloseApp(object param)
        {
            Application.Current.Shutdown();
        }

        private void SortList(object param)
        { 
            //TODO zrob sortowanie po przekazanym obiekcie

            string[] parameters = param.ToString().Split(' ');
            string columnName = parameters[0];
            string direction = parameters[1];

            //Walidacja:
            if ((direction != "Ascending") && (direction != "Descending")) return;

            ListSortDirection listSortDirection = new ListSortDirection();

            switch (direction)
	        {
                case "Ascending":
                    listSortDirection = ListSortDirection.Ascending;
                    break;
                
                case "Descending":
                    listSortDirection = ListSortDirection.Descending;
                    break;

		        default:
                    break;
	        }

            switch (columnName)
            {
                case "Nazwa":
                    ProductList.Sort(x => x.Nazwa, listSortDirection);
                    break;
                case "CenaZakupu":
                    ProductList.Sort(x => x.CenaZakupu, listSortDirection);
                    break;
                case "CenaSprzedazy":
                    ProductList.Sort(x => x.CenaSprzedazy, listSortDirection);
                    break;
                case "Kategoria":
                    ProductList.Sort(x => x.Kategoria, listSortDirection);
                    break;
                case "Ilosc":
                    ProductList.Sort(x => x.Popularnosc, listSortDirection);
                    break;
                case "CzyKupowac":
                    ProductList.Sort(x => x.CzyKupowac, listSortDirection);
                    break;
                default:
                    break;
            }
            
        }

        private bool CanShowListWindowCommand(object param)
        {
            if (IsBusy == true) return false;
            //TODO Popraw:
            if (ProductList.Count == 0) return false;

            foreach (Product product in ProductList)
            {
                if (!product.IsValid)
                    return false;
            }
            return true;
            
        }

        private bool CanRemoveCommand(object param)
        {
            if (IsBusy == true) return false;
            if (SelectedProduct != null)
                return true;
            else
                return false;
        }

        private bool CanMoveCommand(object param)
        {
            if (IsBusy == true) return false;
            if (SelectedProduct != null)
                return true;
            else
                return false;
        }

        private bool CanSortListCommand(object param)
        {
            if (ProductList.Count == 0) return false;
            if (IsBusy == true) return false;
            
            return true;
        }
    }

        #endregion

}
