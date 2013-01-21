using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proj.Model;

namespace Proj.Data
{
    public class ProductsCollection<T> : ObservableCollection<T> 
        where T : Product
    {

        #region Constructors

        public ProductsCollection()
            : base()
        {
            this.CollectionChanged += ContentCollectionChanged;
        }

        public ProductsCollection(List<T> list)
            : base(list)
        {
            this.CollectionChanged += ContentCollectionChanged;
        }

        public ProductsCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.CollectionChanged += ContentCollectionChanged;
        }

        #endregion

        #region Products PropertyChanged Notifications

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in e.OldItems)
                {
                    item.PropertyChanged -= ProductsPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in e.NewItems)
                {
                    item.PropertyChanged += ProductsPropertyChanged;
                }
            }
        }

        public void ProductsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;

            switch (propertyName)
            {
                case "CzyKupowac":
                    OnPropertyChanged("ProductsToBuyCount");

                    OnPropertyChanged("ProductsPurchaseCost");
                    OnPropertyChanged("ProductsSaleEarn");
                    break;

                case "CenaZakupu":
                    OnPropertyChanged("ProductsPurchaseCost");
                    OnPropertyChanged("ProductsSaleEarn");
                    break;

                case "CenaSprzedazy":
                    OnPropertyChanged("ProductsPurchaseCost");
                    OnPropertyChanged("ProductsSaleEarn");
                    break;

                case "Popularnosc":
                    OnPropertyChanged("ProductsPurchaseCost");
                    OnPropertyChanged("ProductsSaleEarn");
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Overrides Base Class Methods

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnPropertyChanged("ProductsCount");

            OnPropertyChanged("ProductsPurchaseCost");
            OnPropertyChanged("ProductsSaleEarn");
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnPropertyChanged("ProductsCount");
            OnPropertyChanged("ProductsToBuyCount");

            OnPropertyChanged("ProductsPurchaseCost");
            OnPropertyChanged("ProductsSaleEarn");
        }

        protected override event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this ,new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Extension Methods

        public void Sort<TKey>(Func<T, TKey> keySelector, System.ComponentModel.ListSortDirection direction)
        {
            switch (direction)
            {
                case System.ComponentModel.ListSortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case System.ComponentModel.ListSortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            ApplySort(Items.OrderBy(keySelector, comparer));
        }

        private void ApplySort(IEnumerable<T> sortedItems)
        {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }
        #endregion

        #region Properties

        public int ProductsCount
        {
            get 
            {
                return this.Count();
            }

        }

        public int ProductsToBuyCount
        {
            get
            {
                int count = 0;
                foreach (T product in this)
                {   
                    if (product.CzyKupowac) count++;
                }
                return count;
            }
        }

        public decimal ProductsPurchaseCost
        {
            get
            {
                decimal totalCost = 0;
                foreach (T product in this)
                {
                    if (product.CzyKupowac == true)
                    {
                        totalCost += product.CenaZakupu * product.Popularnosc;
                    }
                }
                return totalCost;
            }
        }

        public decimal ProductsSaleEarn
        {
            get
            {
                decimal totalEarnings = 0;
                foreach (T product in this)
                {
                    if (product.CzyKupowac == true)
                    {
                        totalEarnings += product.CenaSprzedazy * product.Popularnosc;
                    }
                }
                return totalEarnings;
            }
        }

        #endregion
    }
}
