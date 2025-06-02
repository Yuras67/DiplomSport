using KP.Add_Folder;
using KP.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KP.Edit_Folder;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;

namespace KP.Admin_Folder
{
    public partial class Ad_Client_Page : System.Windows.Controls.Page
    {
        public Ad_Client_Page()
        {
            InitializeComponent();
            AdDGrid_Clients.ItemsSource = DiplomSportEntities.GetContext().Clients.ToList();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Client add_Client = new Add_Client();
            add_Client.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_Client edit_Client = new Edit_Client((sender as System.Windows.Controls.Button).DataContext as Clients);
                edit_Client.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var clientRemoving = AdDGrid_Clients.SelectedItems.Cast<Clients>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DiplomSportEntities.GetContext().Clients.RemoveRange((IEnumerable<Clients>)clientRemoving);
                    DiplomSportEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    AdDGrid_Clients.ItemsSource = DiplomSportEntities.GetContext().Clients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            Word.Application word = new Word.Application();
            word.Visible = false;
            Word.Document document = word.Documents.Add();

            Word.Paragraph para = document.Content.Paragraphs.Add();
            para.Range.Text = "Отчет о клиентах";

            Word.Table table = document.Tables.Add(para.Range, (AdDGrid_Clients.Items.Count - 1) + 1, (AdDGrid_Clients.Columns.Count - 1));
            table.Borders.Enable = 1;

            for (int j = 0; j < (AdDGrid_Clients.Columns.Count - 1); j++)
            {
                table.Cell(1, j + 1).Range.Text = AdDGrid_Clients.Columns[j].Header.ToString();
            }

            for (int i = 0; i < (AdDGrid_Clients.Items.Count - 1); i++)
            {
                for (int j = 0; j < (AdDGrid_Clients.Columns.Count - 1); j++)
                {
                    TextBlock b = AdDGrid_Clients.Columns[j].GetCellContent(AdDGrid_Clients.Items[i]) as TextBlock;
                    table.Cell(i + 2, j + 1).Range.Text = b.Text;
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                document.SaveAs2(filePath);
                document.Close();
                word.Quit();
            }
        }

        private void Button_Click_Excel(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;
            Workbook workbook = excel.Workbooks.Add();
            Worksheet worksheet = workbook.ActiveSheet;
            if (AdDGrid_Clients != null)
            {
                for (int j = 0; j < (AdDGrid_Clients.Columns.Count - 1); j++)
                {
                    worksheet.Cells[2, j + 1] = AdDGrid_Clients.Columns[j].Header.ToString();
                }

                for (int i = 0; i < AdDGrid_Clients.Items.Count; i++)
                {
                    for (int j = 0; j < (AdDGrid_Clients.Columns.Count - 1); j++)
                    {
                        TextBlock b = AdDGrid_Clients.Columns[j].GetCellContent(AdDGrid_Clients.Items[i]) as TextBlock;
                        worksheet.Cells[i + 2, j + 1] = b.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("datagridkolvo не инициализирован или равен null.");
                worksheet.Cells[1, 1] = "Отчет о клиентах";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                workbook.SaveAs(filePath);
                workbook.Close();
                excel.Quit();
            }
        }
    }
}
