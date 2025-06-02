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
    /// Логика взаимодействия для Ad_WorkoutBooking_Page.xaml
    /// </summary>
    public partial class Ad_WorkoutBooking_Page : System.Windows.Controls.Page
    {
        public Ad_WorkoutBooking_Page()
        {
            InitializeComponent();
            AdDGrid_WorkoutB.ItemsSource = DiplomSportEntities.GetContext().WorkoutBookings.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_WorkoutBooking add_WorkoutBooking = new Add_WorkoutBooking();
            add_WorkoutBooking.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_WorkoutBooking edit_WorkoutBooking = new Edit_WorkoutBooking((sender as System.Windows.Controls.Button).DataContext as WorkoutBookings);
                edit_WorkoutBooking.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var workoutBRemoving = AdDGrid_WorkoutB.SelectedItems.Cast<WorkoutBookings>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {workoutBRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DiplomSportEntities.GetContext().WorkoutBookings.RemoveRange((IEnumerable<WorkoutBookings>)workoutBRemoving);
                    DiplomSportEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    AdDGrid_WorkoutB.ItemsSource = DiplomSportEntities.GetContext().WorkoutBookings.ToList();
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
            para.Range.Text = "Отчет о бронировании";

            Word.Table table = document.Tables.Add(para.Range, (AdDGrid_WorkoutB.Items.Count - 1) + 1, (AdDGrid_WorkoutB.Columns.Count - 1));
            table.Borders.Enable = 1;

            for (int j = 0; j < (AdDGrid_WorkoutB.Columns.Count - 1); j++)
            {
                table.Cell(1, j + 1).Range.Text = AdDGrid_WorkoutB.Columns[j].Header.ToString();
            }

            for (int i = 0; i < (AdDGrid_WorkoutB.Items.Count - 1); i++)
            {
                for (int j = 0; j < (AdDGrid_WorkoutB.Columns.Count - 1); j++)
                {
                    TextBlock b = AdDGrid_WorkoutB.Columns[j].GetCellContent(AdDGrid_WorkoutB.Items[i]) as TextBlock;
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
            if (AdDGrid_WorkoutB != null)
            {
                for (int j = 0; j < (AdDGrid_WorkoutB.Columns.Count - 1); j++)
                {
                    worksheet.Cells[2, j + 1] = AdDGrid_WorkoutB.Columns[j].Header.ToString();
                }

                for (int i = 0; i < AdDGrid_WorkoutB.Items.Count; i++)
                {
                    for (int j = 0; j < (AdDGrid_WorkoutB.Columns.Count - 1); j++)
                    {
                        TextBlock b = AdDGrid_WorkoutB.Columns[j].GetCellContent(AdDGrid_WorkoutB.Items[i]) as TextBlock;
                        worksheet.Cells[i + 2, j + 1] = b.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("datagridkolvo не инициализирован или равен null.");
                worksheet.Cells[1, 1] = "Отчет о бронировании";
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
