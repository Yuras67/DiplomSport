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
    /// Логика взаимодействия для Edit_Trainer.xaml
    /// </summary>
    public partial class Edit_Trainer : Window
    {
        Trainers _currentTrainer = new Trainers();

        public Edit_Trainer(Trainers selectedTrainer)
        {
            InitializeComponent();
            DataContext = _currentTrainer;

            if (selectedTrainer != null)
                _currentTrainer = selectedTrainer;
            DataContext = _currentTrainer;
        }
        private void Button_Click(object sender, object e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTrainer.TrainerID == 0)
                DiplomSportEntities.GetContext().Trainers.Add(_currentTrainer);

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