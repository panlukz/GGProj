using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Proj.Model;

namespace Proj.Services
{
    class ProcessProductsListService
    {
        public List<Product> GenerateShoppingList(IEnumerable<Product> list)
        {
            List<Product> GeneratedShoppingList = new List<Product>();

            foreach (Product produkt in list)
            {
                if (produkt.CzyKupowac == true)
                {
                    GeneratedShoppingList.Add(
                    new Product {
                        Nazwa = produkt.Nazwa,
                        JednostkaSprzedazy = produkt.JednostkaSprzedazy,
                        MinIloscSprzedazy = produkt.MinIloscSprzedazy,
                        MaxIloscSprzedazy = produkt.MaxIloscSprzedazy,
                        CenaZakupu = produkt.CenaZakupu,
                        CenaSprzedazy = produkt.CenaSprzedazy,
                        Popularnosc = produkt.Popularnosc,
                    });
                }
            }
            return GeneratedShoppingList;
        }

        public IEnumerable<Product> GenerateSellList(IEnumerable<Product> list)
        {
            List<Product> ShoppingList = GenerateShoppingList(list);
            List<Product> GeneratedSellList = new List<Product>();
            Random random = new Random();

            foreach (Product produkt in ShoppingList)
            {

                do
                {
                    // TODO Oczywiscie to do zmiany na jakis ciekawszy algorytm i przedewszystkim wyeliminowac te Inty z przekonwertowanych Decimali
                    // po to aby mozna bylo z nich losowac
                    decimal ilosc = random.Next(Convert.ToInt32(produkt.MinIloscSprzedazy), Convert.ToInt32(produkt.MaxIloscSprzedazy));
                    

                    if (random.Next(1, 10) > 6 && produkt.JednostkaSprzedazy == JednostkiSprzedazy.Kg) ilosc += 0.5m;

                    if (ilosc < produkt.Popularnosc)
                    {
                        
                        produkt.Popularnosc -= ilosc;
                    }
                    else if (ilosc >= produkt.Popularnosc)
                    {
                        ilosc = produkt.Popularnosc;
                        produkt.Popularnosc = 0;
                    }

                    GeneratedSellList.Add(
                    new Product {
                        Nazwa = produkt.Nazwa,
                        JednostkaSprzedazy = produkt.JednostkaSprzedazy,
                        MinIloscSprzedazy = produkt.MinIloscSprzedazy,
                        MaxIloscSprzedazy = produkt.MaxIloscSprzedazy,
                        CenaZakupu = produkt.CenaZakupu,
                        CenaSprzedazy = produkt.CenaSprzedazy,
                        Popularnosc = ilosc,
                    });
                    
                }
                while (produkt.Popularnosc > 0);
            }

            //randomizacja listy sprzedaży
            var RandomizeGeneratedSellList = GeneratedSellList.OrderBy(a => Guid.NewGuid());

            return RandomizeGeneratedSellList;
        }


        
    }
}
