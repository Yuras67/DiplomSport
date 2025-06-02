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

namespace KP.Trainer_Folder
{
    /// <summary>
    /// Логика взаимодействия для Trainer.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {
        public TrainerWindow(string username)
        {
            InitializeComponent();
            Name.Text = username;
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.Tr_Client_Page());
        }
        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.Tr_Trainer_Page());
        }
        private void Services_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.Tr_Service_Page());
        }
        private void Workouts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.Tr_Workout_Page());
        }
        private void WorkoutsBooking_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.Tr_WorkoutBooking_Page());
        }
    }
}
