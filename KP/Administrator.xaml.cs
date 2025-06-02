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
using System.Windows.Shapes;

namespace KP
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator(string username)
        {
            InitializeComponent();
            Name.Text = username;
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_Client_Page());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_User_Page());
        }

        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_Trainer_Page());
        }
        private void Services_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_Service_Page());
        }
        private void Workouts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_Workout_Page());
        }
        private void WorkoutsBooking_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_Folder.Ad_WorkoutBooking_Page());
        }



        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
