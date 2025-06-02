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
    /// Логика взаимодействия для Cl_Trainer_Page.xaml
    /// </summary>
    public partial class Cl_Trainer_Page : Page
    {
        public Cl_Trainer_Page()
        {
            InitializeComponent();
            ClDGrid_Trainers.ItemsSource = DiplomSportEntities.GetContext().Trainers.ToList();
        }
    }
}
