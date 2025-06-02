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
    /// Логика взаимодействия для Tr_Workout_Page.xaml
    /// </summary>
    public partial class Tr_Workout_Page : Page
    {
        public Tr_Workout_Page()
        {
            InitializeComponent();
            TrDGrid_Workout.ItemsSource = DiplomSportEntities.GetContext().Workouts.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Workout add_Workout = new Add_Workout();
            add_Workout.ShowDialog();
        }

        private void Button_edit_data(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_Workout edit_Workout = new Edit_Workout((sender as Button).DataContext as Workouts);
                edit_Workout.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}