using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Proj.Model
{
    public class Product : IDataErrorInfo, INotifyPropertyChanged
    {
        //TODO Ogarnij to co ma być serializowane do XML-a
        //Zaimplementuj do pozostalych wlasciwosci INotifyPropertyChanged
        public int Nr { get; set; }

        private string nazwa;
        public string Nazwa 
        { 
            get
            {
                return nazwa;
            }
            set
            {
                nazwa = value;
                OnPropertyChange("Nazwa");
            }
        }

        private decimal cenaZakupu;
        public decimal CenaZakupu 
        {
            get
            {
                return cenaZakupu;
            }
            set
            {
                cenaZakupu = value;
                OnPropertyChange("CenaZakupu");
                OnPropertyChange("CenaSprzedazy");
                OnPropertyChange("WartoscZakupu");
            }
        }

        private decimal cenaSprzedazy;
        public decimal CenaSprzedazy 
        {
            get
            {
                return cenaSprzedazy;
            }
            set
            {
                cenaSprzedazy = value;
                OnPropertyChange("CenaZakupu");
                OnPropertyChange("CenaSprzedazy");
                OnPropertyChange("WartoscSprzedazy");
            }
        }

        public JednostkiSprzedazy JednostkaSprzedazy { get; set; }

        private decimal minIloscSprzedazy;
        public decimal MinIloscSprzedazy 
        {
            get
            {
                return minIloscSprzedazy;
            }
            set
            {
                minIloscSprzedazy = value;
                OnPropertyChange("MinIloscSprzedazy");
                OnPropertyChange("MaxIloscSprzedazy");
            }
        }

        private decimal maxIloscSprzedazy;
        public decimal MaxIloscSprzedazy 
        {
            get
            {
                return maxIloscSprzedazy;
            }
            set
            {
                maxIloscSprzedazy = value;
                OnPropertyChange("MaxIloscSprzedazy");
                OnPropertyChange("MinIloscSprzedazy");
            }
        }

        public Kategorie Kategoria { get; set; }

        private bool czyKupowac;
        public bool CzyKupowac 
        {
            get
            {
                return czyKupowac;
            }
            set
            {
                czyKupowac = value;
                OnPropertyChange("CzyKupowac");
            }
        }

        private decimal popularnosc;
        public decimal Popularnosc 
        {
            get
            {
                return popularnosc;
            }
            set
            {

                popularnosc = value;
                OnPropertyChange("Popularnosc");
                OnPropertyChange("WartoscSprzedazy");
                OnPropertyChange("WartoscZakupu");
            }
        }

        public decimal WartoscZakupu
        {
            get 
            {
                if (Popularnosc != 0 && CenaZakupu != 0)
                {
                    return Popularnosc * CenaZakupu;
                }
                else
                    return 0;
            }
        }

        public decimal WartoscSprzedazy
        {
            get
            {
                if (Popularnosc != 0 && CenaSprzedazy != 0)
                {
                    return Popularnosc * CenaSprzedazy;
                }
                else
                    return 0;
            }
        }

        
        private bool isValid = true;
        [XmlIgnore]
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }


        //TODO zoptymalizuj te warunki walidacji
        //byc moze prosciej bedzie zrobic BindingGroup i ValidationRules niz DataErrorInfo...
        //szczegolnie w przypadku jak jedna wartosc zalezy od drugiej (cena zakupu i sprzedazy i maxilosc i minilosc...
        #region Obsluga walidacji
        

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case "Nazwa":
                        {
                            if (String.IsNullOrEmpty(Nazwa) || String.IsNullOrWhiteSpace(Nazwa))
                            {
                                error = "Nazwa nie może być pusta lub nie może zawierać samych spacji!";
                                break;
                            }
                            break;
                        }

                    case "CenaZakupu":
                        {
                            if (CenaZakupu > CenaSprzedazy)
                            {
                                error = "Cena zakupu nie może być większa niż cena sprzedaży!";
                                break;
                            }
                            if (CenaZakupu <= 0)
                            {
                                error = "Cena zakupu nie może być mniejsza lub równa zero!";
                                break;
                            }
                            break;
                        }

                    case "CenaSprzedazy":
                        {
                            if (CenaSprzedazy < CenaZakupu)
                            {
                                error = "Cena sprzedaży nie może być mniejsza niż cena zakupu!";
                                break;
                            }
                            if (CenaSprzedazy <= 0)
                            {
                                error = "Cena sprzedaży nie może być mniejsza lub równa zero!";
                                break;
                            }
                            break;
                        }

                    case "MinIloscSprzedazy":
                        {
                            if (MinIloscSprzedazy > MaxIloscSprzedazy)
                            {
                                error = "Minimalna ilość sprzedaży nie może być większa niż maksymalna!";
                                break;
                            }
                            if (MinIloscSprzedazy <= 0)
                            {
                                error = "Minimalna ilość sprzedaży nie może być mniejsza lub równa zero!";
                                break;
                            }
                            break;
                        }

                    case "MaxIloscSprzedazy":
                        {
                            if (MaxIloscSprzedazy < MinIloscSprzedazy)
                            {
                                error = "Maksymalna ilość sprzedaży nie może być mniejsza niż minimalna!";
                                break;
                            }
                            if (MaxIloscSprzedazy <= 0)
                            {
                                error = "Maksymalna ilość sprzedaży nie może być mniejsza lub równa zero!";
                                break;
                            }
                            break;
                        }
                    case "Popularnosc":
                        {
                            if (Popularnosc < 1)
                            {
                                error = "Ilość musi być większa od zera!";
                                break;
                            }
                            break;
                        }
                }

                if (string.IsNullOrEmpty(error))
                    IsValid = true;
                else
                    IsValid = false;

                return error;
            }
        }

        public string Error
        {
            get { return null; }
        }
        #endregion 



    
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum Kategorie
    {
        Warzywa,
        Owoce,
        Kiszonki,
        Inne
    }

    public enum JednostkiSprzedazy
    {
        Kg,
        Szt,
    }

}
