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
    /// Логика взаимодействия для Tr_WorkoutBooking_Page.xaml
    /// </summary>
    public partial class Tr_WorkoutBooking_Page : Page
    {
        public Tr_WorkoutBooking_Page()
        {
            InitializeComponent();
            TrDGrid_WorkoutB.ItemsSource = DiplomSportEntities.GetContext().WorkoutBookings.ToList();
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
                Edit_WorkoutBooking edit_WorkoutBooking = new Edit_WorkoutBooking((sender as Button).DataContext as WorkoutBookings);
                edit_WorkoutBooking.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}