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
    /// Логика взаимодействия для Add_Service.xaml
    /// </summary>
    public partial class Add_Service : Window
    {
        Services _currentService = new Services();

        public Add_Service()
        {
            InitializeComponent();
            DataContext = _currentService;
        }

        private void Button_Click(object sender, object e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentService.ServiceID == 0)
                DiplomSportEntities.GetContext().Services.Add(_currentService);

            try
            {
                DiplomSportEntities.GetContext().SaveChanges();
                MessageBox.Show("Услуга создана");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}