using System.Linq;
using System.Windows;
using PromtWPF.ModelDB;

namespace PromtWPF.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var db = new PromtEntities1())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Авторизация успешна
                    ProductsWindow productsWindow = new ProductsWindow(user.RoleId);
                    productsWindow.Show();
                    this.Close(); // Закрываем LoginWindow
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.");
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}