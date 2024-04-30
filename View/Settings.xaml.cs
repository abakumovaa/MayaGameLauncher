using MayaGameLauncher.ViewModel;
using MayaGameLauncher.Utilities;
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
using MayaGameLauncher.ClassHelper;
using System.Text.RegularExpressions;

namespace MayaGameLauncher.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = new SettingVM();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newUserName = UserNameTextBox.Text.Trim();

            // Проверка длины ника
            if (newUserName.Length < 3)
            {
                MessageBox.Show("Имя пользователя должно быть длиннее 2 символов.");
                return;
            }

            var user = EF.Context.User.FirstOrDefault(u => u.ID == SessionInfo.CurrentUserID);
            if (user != null)
            {
                // Проверка на то, что новый никнейм отличается от текущего
                if (user.UserName == newUserName)
                {
                    MessageBox.Show("Это уже ваш текущий никнейм.");
                    return;
                }

                // Проверка на уникальность ника
                if (EF.Context.User.Any(u => u.UserName == newUserName))
                {
                    MessageBox.Show("Такой никнейм уже занят.");
                    return;
                }

                // Обновление никнейма пользователя
                user.UserName = newUserName;
                EF.Context.SaveChanges();

                // Обновление текущего имени пользователя в сессии
                SessionInfo.CurrentUserName = newUserName; // Это вызовет событие и обновит имя в главном меню

                MessageBox.Show("Имя пользователя успешно обновлено.");
            }
        }
    }
}
