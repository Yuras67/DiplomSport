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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP.Client_Folder.Client_Pages
{
    /// <summary>
    /// Логика взаимодействия для Cl_Workout_Page.xaml
    /// </summary>
    public partial class Cl_Workout_Page : Page
    {
        public Cl_Workout_Page()
        {
            InitializeComponent();
            ClDGrid_Workout.ItemsSource = DiplomSportEntities.GetContext().Workouts.ToList();
        }
    }
}
