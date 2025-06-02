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
    /// Логика взаимодействия для Tr_Client_Page.xaml
    /// </summary>
    public partial class Tr_Client_Page : Page
    {
        public Tr_Client_Page()
        {
            InitializeComponent();
            TrDGrid_Clients.ItemsSource = DiplomSportEntities.GetContext().Clients.ToList();
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
                Edit_Client edit_Client = new Edit_Client((sender as Button).DataContext as Clients);
                edit_Client.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}