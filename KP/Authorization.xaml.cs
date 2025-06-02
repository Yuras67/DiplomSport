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

namespace KP
{
    public partial class Authorization : Window
    {
        private string captchaValue;

        public Authorization()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DiplomSportEntities db = new DataBase.DiplomSportEntities();
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Все поля обязательны для заполнения");
                GenerateCaptcha();
                return;
            }

            var currentUser = db.Users.FirstOrDefault(u => u.Username == login && u.Password == password);

            if (currentUser == null)
            {
                MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                GenerateCaptcha();
                return;
            }

            if (CaptchaTextBox.Text.Equals(captchaValue, StringComparison.OrdinalIgnoreCase) || CaptchaTextBox.Text == "")
            {
                if (currentUser.Role == "admin")
                {
                    Administrator administratorWindow = new Administrator(currentUser.Username);
                    this.Close();
                    administratorWindow.ShowDialog();
                }
                else if (currentUser.Role == "trainer")
                {
                    Trainer_Folder.TrainerWindow trainerWindow = new Trainer_Folder.TrainerWindow(currentUser.Username);
                    this.Close();
                    trainerWindow.ShowDialog();
                }
                else if (currentUser.Role == "client")
                {
                    Client_Folder.ClientWindow clientWindow = new Client_Folder.ClientWindow(currentUser.Username);
                    this.Close();
                    clientWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                    GenerateCaptcha();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неверную капчу. Пожалуйста проверьте ещё раз");
                GenerateCaptcha();
                return;
            }
        }

        private void ShowCaptcha()
        {
            CaptchaPanel.Visibility = Visibility.Visible;
            CaptchaTextBlock.Text = captchaValue;
        }

        private void GenerateCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            StringBuilder captchaStringBuilder = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                captchaStringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            captchaValue = captchaStringBuilder.ToString();
            ShowCaptcha();
        }

        private void Update_Captcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
