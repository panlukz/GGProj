using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proj.Model;
using Proj.Commands;
using Proj.Services;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Input;


namespace Proj.ViewModels
{
    public class ListWindowViewModel : ViewModelBase
    {

        private IEnumerable<Product> productList;

        private List<string> listColumns;

        private List<string> shoppingListColumns = new List<string>() { "Nazwa", "Cena zakupu", "Ilość", "Jednostka", "Wartość zakupu" };
        private List<string> sellListColumns = new List<string>() { "Nazwa", "Cena sprzedaży", "Ilość", "Jednostka", "Wartość sprzedaży" };

        #region Constructor

        public ListWindowViewModel(string listType)
        {
            switch (listType)
            {
                case "shoppingList":
                    ListColumns = shoppingListColumns;
                    break;
                case "sellList":
                    ListColumns = sellListColumns;
                    break;
                default:
                    break;
            }
        }
        
        #endregion

        #region Properties

        public IEnumerable<Product> ProductList 
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
            }
        }

        public List<string> ShoppingListColumns
        {
            get
            {
                return shoppingListColumns;
            }
            set
            {
                shoppingListColumns = value;
            }
        }

        public List<string> SellListColumns
        {
            get
            {
                return sellListColumns;
            }
            set
            {
                sellListColumns = value;
            }
        }

        public List<string> ListColumns
        {
            get
            {
                return listColumns;
            }
            set
            {
                listColumns = value;
            }
        }

        #endregion

        #region Commands

        private RelayCommand printCommand;
        public ICommand PrintCommand
        {
            get
            {
                if (this.printCommand == null)
                {
                    this.printCommand = new RelayCommand(this.PrintList);
                }
                return this.printCommand;
            }
        }

        #endregion

        #region Methods

        private void PrintList(object param)
        {

            FlowDocument flowDocument = new FlowDocument();
            Table ewidencjaTable = new Table();

            flowDocument.Blocks.Add(ewidencjaTable);

            int numberOfColumns = 5;

            for (int c = 0; c < numberOfColumns; c++)
                ewidencjaTable.Columns.Add(new TableColumn());

            TableRow thr = new TableRow();
            thr.Cells.Add(new TableCell(new Paragraph(new Run("Nazwa"))));
            thr.Cells.Add(new TableCell(new Paragraph(new Run("Jednostka Sprzedazy"))));
            thr.Cells.Add(new TableCell(new Paragraph(new Run("Cena"))));
            thr.Cells.Add(new TableCell(new Paragraph(new Run("Ilość"))));
            thr.Cells.Add(new TableCell(new Paragraph(new Run("Wartość"))));

            TableRowGroup thrg = new TableRowGroup();
            thrg.Rows.Add(thr);
            ewidencjaTable.RowGroups.Add(thrg);

            foreach (Product produkt in ProductList)
            {
                TableRow tr = new TableRow();

                tr.Cells.Add(new TableCell(new Paragraph(new Run(produkt.Nazwa))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(produkt.JednostkaSprzedazy.ToString()))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(produkt.CenaSprzedazy.ToString() + " zł"))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(produkt.Popularnosc.ToString()))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run((produkt.Popularnosc * produkt.CenaSprzedazy).ToString() + " zł"))));


                TableRowGroup trg = new TableRowGroup();
                trg.Rows.Add(tr);
                ewidencjaTable.RowGroups.Add(trg);
            }

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                flowDocument.ColumnWidth = pd.PrintableAreaWidth;

                IDocumentPaginatorSource idocument = flowDocument as IDocumentPaginatorSource;

                pd.PrintDocument(idocument.DocumentPaginator, "lista");

            }
            else
            {
                return;
            }




        }

        #endregion


    }
}
