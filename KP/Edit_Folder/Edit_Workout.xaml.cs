using KP.DataBase;
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

namespace KP.Edit_Folder
{
    /// <summary>
    /// Логика взаимодействия для Edit_Workout.xaml
    /// </summary>
    public partial class Edit_Workout : Window
    {
        Workouts _currentWorkout = new Workouts();

        public Edit_Workout(Workouts selectedWorkout)
        {
            InitializeComponent();
            DataContext = _currentWorkout;

            if (selectedWorkout != null)
                _currentWorkout = selectedWorkout;
            DataContext = _currentWorkout;
        }
        private void Button_Click(object sender, object e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentWorkout.WorkoutID == 0)
                DiplomSportEntities.GetContext().Workouts.Add(_currentWorkout);

            try
            {
                DiplomSportEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные изменены");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}