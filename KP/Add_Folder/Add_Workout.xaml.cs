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

namespace KP.Add_Folder
{
    /// <summary>
    /// Логика взаимодействия для Add_Workout.xaml
    /// </summary>
    public partial class Add_Workout : Window
    {
        Workouts _currentWorkout = new Workouts();

        public Add_Workout()
        {
            InitializeComponent();
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
                MessageBox.Show("Тренировка создан");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}