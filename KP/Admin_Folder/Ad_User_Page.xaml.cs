using KP.Add_Folder;
using KP.DataBase;
using KP.Edit_Folder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;

namespace KP.Admin_Folder
{
    public partial class Ad_User_Page : System.Windows.Controls.Page
    {
        public Ad_User_Page()
        {
            InitializeComponent();
            AdDGrid_Users.ItemsSource = DiplomSportEntities.GetContext().Users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_User add_User = new Add_User();
            add_User.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_User edit_User = new Edit_User((sender as System.Windows.Controls.Button).DataContext as Users);
                edit_User.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var userRemoving = AdDGrid_Users.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {userRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DiplomSportEntities.GetContext().Clients.RemoveRange((IEnumerable<Clients>)userRemoving);
                    DiplomSportEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    AdDGrid_Users.ItemsSource = DiplomSportEntities.GetContext().Clients.ToList();
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
            para.Range.Text = "Отчет о пользователях";

            Word.Table table = document.Tables.Add(para.Range, (AdDGrid_Users.Items.Count - 1) + 1, (AdDGrid_Users.Columns.Count - 1));
            table.Borders.Enable = 1;

            for (int j = 0; j < (AdDGrid_Users.Columns.Count - 1); j++)
            {
                table.Cell(1, j + 1).Range.Text = AdDGrid_Users.Columns[j].Header.ToString();
            }

            for (int i = 0; i < (AdDGrid_Users.Items.Count - 1); i++)
            {
                for (int j = 0; j < (AdDGrid_Users.Columns.Count - 1); j++)
                {
                    TextBlock b = AdDGrid_Users.Columns[j].GetCellContent(AdDGrid_Users.Items[i]) as TextBlock;
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
            if (AdDGrid_Users != null)
            {
                for (int j = 0; j < (AdDGrid_Users.Columns.Count - 1); j++)
                {
                    worksheet.Cells[2, j + 1] = AdDGrid_Users.Columns[j].Header.ToString();
                }

                for (int i = 0; i < AdDGrid_Users.Items.Count; i++)
                {
                    for (int j = 0; j < (AdDGrid_Users.Columns.Count - 1); j++)
                    {
                        TextBlock b = AdDGrid_Users.Columns[j].GetCellContent(AdDGrid_Users.Items[i]) as TextBlock;
                        worksheet.Cells[i + 2, j + 1] = b.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("datagridkolvo не инициализирован или равен null.");
                worksheet.Cells[1, 1] = "Отчет о пользователях";
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
