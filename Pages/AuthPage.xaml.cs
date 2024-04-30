using MayaGameLauncher.ClassHelper;
using MayaGameLauncher;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.IO;

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
            var userAuth = ClassHelper.EF.Context.User.FirstOrDefault(u => u.Email == tb1.Text && u.Password == tb2.Password);
            if (userAuth != null)
            {
                SessionInfo.CurrentUserName = userAuth.UserName;
                SessionInfo.CurrentUserID = userAuth.ID;
                SessionInfo.CurrentBalance = (decimal)userAuth.UserBalance;

                // Проверяем, есть ли сохраненный путь к изображению
                if (!string.IsNullOrEmpty(userAuth.PhotoPath) && File.Exists(userAuth.PhotoPath))
                {
                    // Загрузка и установка пользовательского изображения
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainMenu mainMenu = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault();
                        if (mainMenu != null)
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(userAuth.PhotoPath));
                            mainMenu.Img.ImageSource = bitmap;  // Обновляем ImageSource у ImageBrush, который используется в Ellipse
                        }
                    });
                }

                // Отображаем MainMenu или продолжаем с текущим
                MainMenu mainMenuToShow = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault() ?? new MainMenu();
                mainMenuToShow.Show();
            }
            else
            {
                MessageBox.Show("Данный пользователь не найден в базе данных. Пожалуйста, попробуйте ввести данные еще раз", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
