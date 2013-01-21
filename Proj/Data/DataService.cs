using Proj.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Proj.Data
{
    class DataService
    {
        public ProductsCollection<Product> OpenCollectionFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductsCollection<Product>));

            StreamReader reader = null;

            ProductsCollection<Product> list = null;
            reader = new StreamReader(path);

            try
            {
                list = (ProductsCollection<Product>)serializer.Deserialize(reader);
            }
            catch (System.InvalidOperationException ex)
            {
                //TODO dopracuj ten catch ?
                throw new ApplicationException("Plik ma nieodpowiedni format lub jest uszkodzony", ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return list;
        }

        public void SaveCollectionToFile(ProductsCollection<Product> productsCollection, string path)
        {
            XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(productsCollection.GetType());

            TextWriter txtW = new StreamWriter(path);
            serializer.Serialize(txtW, productsCollection);
            txtW.Close();
        }
    }
}
