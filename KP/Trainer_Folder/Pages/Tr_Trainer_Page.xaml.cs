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

namespace KP.Trainer_Folder.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tr_Trainer_Page.xaml
    /// </summary>
    public partial class Tr_Trainer_Page : Page
    {
        public Tr_Trainer_Page()
        {
            InitializeComponent();
            TrDGrid_Trainers.ItemsSource = DiplomSportEntities.GetContext().Trainers.ToList();
        }
    }
}
