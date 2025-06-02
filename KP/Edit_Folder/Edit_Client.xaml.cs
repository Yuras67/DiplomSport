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
    /// Логика взаимодействия для Edit_Client.xaml
    /// </summary>
    public partial class Edit_Client : Window
    {
        private Clients _currentClient = new Clients();
        public Edit_Client(Clients selectedClient)
        {
            InitializeComponent();
            DataContext = _currentClient;

            if (selectedClient != null)
                _currentClient = selectedClient;
            DataContext = _currentClient;
        }
        private void Button_Click(object sender, object e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentClient.ClientID == 0)
                DiplomSportEntities.GetContext().Clients.Add(_currentClient);

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