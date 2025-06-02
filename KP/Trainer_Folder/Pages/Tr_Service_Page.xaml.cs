using KP.Add_Folder;
using KP.DataBase;
using KP.Edit_Folder;
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

namespace KP.Trainer_Folder.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tr_Service_Page.xaml
    /// </summary>
    public partial class Tr_Service_Page : Page
    {
        public Tr_Service_Page()
        {
            InitializeComponent();
            TrDGrid_Service.ItemsSource = DiplomSportEntities.GetContext().Services.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Service add_Service = new Add_Service();
            add_Service.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_Service edit_Service = new Edit_Service((sender as Button).DataContext as Services);
                edit_Service.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}