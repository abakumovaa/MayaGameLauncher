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

namespace MayaGameLauncher
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbGender.DisplayMemberPath = "Title";
            CmbGender.SelectedIndex = 0;
        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
            }
        }

        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }

            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        private void CreateAcc(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Заполните поле Email");
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstName.Text))
            {
                MessageBox.Show("Заполните ваше имя");
                return;
            }

            if (string.IsNullOrWhiteSpace(LastName.Text))
            {
                MessageBox.Show("Заполните вашу фамилию");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                MessageBox.Show("Заполните ваш никнейм игрока");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Заполните ваш пароль");
                return;
            }

            if (Password.Password.Length < 8)
            {
                MessageBox.Show("Ваш пароль слишком короткий! Пожалуйста, введите пароль, состоящий из 8 и более символов");
                return;
            }

            // добавление данных в бд

            DB.User addUser = new DB.User
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                UserName = UserName.Text,
                Phone = Phone.Text,
                Password = Password.Password,
                GenderCode = (CmbGender.SelectedItem as DB.Gender).Code
            };

            ClassHelper.EF.Context.User.Add(addUser);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Вы были успешно зарегистрированы в системе!");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
