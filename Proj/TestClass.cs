using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj.Model;

namespace Proj
{
    public class TestClass
    {
        public List<Product> randomProductsList = new List<Product>();

        public TestClass()
        {
            string productsNames = "Brokuł&Jarmuż&Kalafior&Kalarepa&Kapusta brukselska&Brukselka&Kapusta chińska&Kapusta głowiasta biała&Kapusta głowiasta czerwona&Kapusta pekińska&Kapusta włoska&Cebula perłowa&Cebula zwyczajna&Cebula kartoflanka&Cebula wielopiętrowa&Czosnekpospolity&Por&Rokambuł&Szalotka&Siedmiolatka&Szczypiorek&Burak liściowy&Burak ćwikłowy&Cykoria sałatowa&Cykoria endywia&Pietruszka naciowa&Portulaka warzywna&Roszponka warzywna&Pieprzyca siewna&Rzeżucha ogrodowa&Rukiew wodna&Sałata siewna&Seler naciowy&Szpinak zwyczajny&Szpinak nowozelandzki&Rukola, rokietta siewna&Mniszek lekarski&Batat, wilec ziemniaczany&Burak ćwikłowy&Maniok jadalny&Maranta&Marchew zwyczajna&Pasternak zwyczajny&Pietruszka korzeniowa&Salep&Salsefia&Seler korzeniowy&Słonecznik bulwiasty&Wężymord czarny korzeń&Oberżyna, bakłażan&Papryka roczna&Pomidor&Ziemniak&Rodzynek brazylijski&Miechunka peruwiańska&Miechunka pomidorowa&Dynia &Kabaczek&Cukinia&Patison&Dynia makaronowa&Arbuz&Melon&Ogórek&Brukiew&Rzepa&Rzodkiew&Rzodkiewka&Rzodkiew japońska&Daikon&Bób&Ciecierzyca pospolita&Fasola zwykła&Fasola wielokwiatowa&Groch&Soczewica jadalna&Soja&Chrzan pospolity&Rabarbar ogrodowy&Szczaw zwyczajny&Kapary&Karczoch zwyczajny&Kard&Koper ogrodowy&Kukurydza cukrowa&Majeranek ogrodowy&Oliwka europejska&Pochrzyn chiński&Szparag lekarski&Pieczarka&Pieprznik jadalny&Borowikowate&Trufle&Uszaki&Trzęsaki&Brzoskwinia&Czereśnia&Gruszka&Jabłko&Orzech laskowy&Morela&Nektaryna&Orzech włoski&Śliwka&Wiśnia&Cytryna&Figa&Granat&Grejpfrut&Hurma&Karambola&Kasztan jadalny&Kumkwat&Mandarynka&Mango&Kiwi&Migdał&Oliwka&Daktyl&Kokos&Palma orzechowa&Pomarańcza&Rambutan&Awokado&Agrest&Aronia&Borówka amerykańska&Borówka bagienna&Borówka brusznica&Borówka czarna&Jeżyna&Jagoda kamczacka&Malina&Malinojeżyna&Porzeczka czarna &Porzeczka czerwona&Porzeczka biała&Winogrona&Żurawina&Ananas&Banan zwyczajny&Poziomka&Truskawka&Arbuz&Durian&Flaszowiec&Gwajawa&Mangostan właściwy&Nanercz zachodni&Pigwa&Obierania&Drylowania&Szypułkowania";

            char splitBy = '&';

            List<string> productsNamesList = productsNames.Split(splitBy).ToList();
            

            Random random = new Random();

            foreach (string item in productsNamesList)
            {

                decimal purchasePrice = 0M;
                decimal sellPrice = 0M;

                decimal decA = Convert.ToDecimal(random.Next(2, 6)) - Convert.ToDecimal(random.Next(1, 5)) * 2 / 10;
                decimal decB = Convert.ToDecimal(random.Next(2, 6)) - Convert.ToDecimal(random.Next(1, 5)) * 2 / 10;

                if (decA > decB)
                {
                    purchasePrice = decB;
                    sellPrice = decA;
                }
                else if (decB > decA)
                {
                    purchasePrice = decA;
                    sellPrice = decB;
                }
                else if (decA == decB)
                {
                    purchasePrice = decA;

                    decimal helper = Convert.ToDecimal(random.Next(2, 4)) / 10;
                    sellPrice = decA + helper * decA;
                }

                decimal minSalesNumber = 0M;
                decimal maxSalesNumber = 0M;

                decimal decC = Convert.ToDecimal(random.Next(1, 4));
                decimal decD = Convert.ToDecimal(random.Next(5, 8));

                if (decC > decD)
                {
                    minSalesNumber = decD;
                    maxSalesNumber = decC;
                }
                else if (decD > decC)
                {
                    minSalesNumber = decC;
                    maxSalesNumber = decD;
                }
                else if (decC == decD)
                {
                    minSalesNumber = decC;

                    decimal helper = random.Next(1, 5);
                    maxSalesNumber = decC + helper * decC;
                }


                int productsCount = random.Next(5, 10);


                randomProductsList.Add(
                    new Product {
                        Nazwa = item,
                        CenaZakupu = purchasePrice,
                        CenaSprzedazy = sellPrice,
                        MinIloscSprzedazy = minSalesNumber,
                        MaxIloscSprzedazy = maxSalesNumber,
                        Popularnosc = productsCount, 
                    });
            }
        }
    }
        
}

