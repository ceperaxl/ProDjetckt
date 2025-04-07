using System.Linq;
using System.Windows;
using PromtWPF.ModelDB;
using System.Windows.Controls;

namespace PromtWPF.View
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            RoleComboBox.SelectedIndex = 1; // Default to Manager
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            int roleId = int.Parse(((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString());

            using (var db = new PromtEntities1())
            {
                // Проверяем, существует ли уже пользователь с таким именем
                if (db.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Пользователь с таким именем уже существует.");
                    return;
                }

                // Создаем нового пользователя
                //  !!!  ВАЖНО: Используйте класс User, сгенерированный EDM  !!!
                PromtWPF.ModelDB.Users newUser = new PromtWPF.ModelDB.Users
                {
                    Username = username,
                    Password = password,
                    RoleId = roleId
                };

                // Добавляем пользователя в контекст и сохраняем изменения
                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}