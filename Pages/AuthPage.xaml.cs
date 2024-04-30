using MayaGameLauncher.ClassHelper;
using MayaGameLauncher;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MayaGameLauncher.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb2.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }

            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            var userAuth = ClassHelper.EF.Context.User.ToList().Where(i => i.Email == tb1.Text && i.Password == tb2.Password).FirstOrDefault();

            if (userAuth != null)
            {
                SessionInfo.CurrentUserName = userAuth.UserName;
                SessionInfo.CurrentUserID = userAuth.ID;
                SessionInfo.CurrentBalance = (decimal)userAuth.UserBalance;

                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
            }

            else
            {
                MessageBox.Show("Данный пользователь не найден в базе данных. Пожалуйста, попробуйте ввести данные еще раз", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
