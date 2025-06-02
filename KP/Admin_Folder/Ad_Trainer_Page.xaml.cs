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
    /// <summary>
    /// Логика взаимодействия для Ad_Trainer_Page.xaml
    /// </summary>
    public partial class Ad_Trainer_Page : System.Windows.Controls.Page
    {
        public Ad_Trainer_Page()
        {
            InitializeComponent();
            AdDGrid_Trainers.ItemsSource = DiplomSportEntities.GetContext().Trainers.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Trainer add_Trainer = new Add_Trainer();
            add_Trainer.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_Trainer edit_Trainer = new Edit_Trainer((sender as System.Windows.Controls.Button).DataContext as Trainers);
                edit_Trainer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var trainerRemoving = AdDGrid_Trainers.SelectedItems.Cast<Trainers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {trainerRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DiplomSportEntities.GetContext().Trainers.RemoveRange((IEnumerable<Trainers>)trainerRemoving);
                    DiplomSportEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    AdDGrid_Trainers.ItemsSource = DiplomSportEntities.GetContext().Trainers.ToList();
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
            para.Range.Text = "Отчет о тренерах";

            Word.Table table = document.Tables.Add(para.Range, (AdDGrid_Trainers.Items.Count - 1) + 1, (AdDGrid_Trainers.Columns.Count - 1));
            table.Borders.Enable = 1;

            for (int j = 0; j < (AdDGrid_Trainers.Columns.Count - 1); j++)
            {
                table.Cell(1, j + 1).Range.Text = AdDGrid_Trainers.Columns[j].Header.ToString();
            }

            for (int i = 0; i < (AdDGrid_Trainers.Items.Count - 1); i++)
            {
                for (int j = 0; j < (AdDGrid_Trainers.Columns.Count - 1); j++)
                {
                    TextBlock b = AdDGrid_Trainers.Columns[j].GetCellContent(AdDGrid_Trainers.Items[i]) as TextBlock;
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
            if (AdDGrid_Trainers != null)
            {
                for (int j = 0; j < (AdDGrid_Trainers.Columns.Count - 1); j++)
                {
                    worksheet.Cells[2, j + 1] = AdDGrid_Trainers.Columns[j].Header.ToString();
                }

                for (int i = 0; i < AdDGrid_Trainers.Items.Count; i++)
                {
                    for (int j = 0; j < (AdDGrid_Trainers.Columns.Count - 1); j++)
                    {
                        TextBlock b = AdDGrid_Trainers.Columns[j].GetCellContent(AdDGrid_Trainers.Items[i]) as TextBlock;
                        worksheet.Cells[i + 2, j + 1] = b.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("datagridkolvo не инициализирован или равен null.");
                worksheet.Cells[1, 1] = "Отчет о тренерах";
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
