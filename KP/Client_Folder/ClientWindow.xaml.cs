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

namespace KP.Client_Folder
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow(string username)
        {
            InitializeComponent();
            Name.Text = username;
        }

        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Client_Pages.Cl_Trainer_Page());
        }
        private void Workouts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Client_Pages.Cl_Workout_Page());
        }
        private void WorkoutsBooking_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Client_Pages.Cl_WorkoutB_Page());
        }
    }
}
