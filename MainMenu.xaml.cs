using MayaGameLauncher.ClassHelper;
using MayaGameLauncher.DB;
using Microsoft.Win32;
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
using MayaGameLauncher.ViewModel;

namespace MayaGameLauncher
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        //private MediaPlayer mediaPlayer = new MediaPlayer();
        private string pathImage = null;
        private List<string> currentUserFriends;
        public MainMenu()
        {
            InitializeComponent();

            //mediaPlayer.Open(new Uri("Sources/soundtrack.mp3", UriKind.Relative));
            //mediaPlayer.MediaEnded += (s, e) => mediaPlayer.Position = TimeSpan.Zero; // Добавление саундтрека
            //mediaPlayer.Play();

            Tb_UserName.Text = SessionInfo.CurrentUserName;
            SessionInfo.OnUserNameChanged += UpdateUserName;
            Tb_Balance_Amount.Text = SessionInfo.CurrentBalance.ToString(); // удалить?
            SessionInfo.OnBalanceChanged += SessionInfo_OnBalanceChanged;

            currentUserFriends = ClassHelper.EF.Context.UserFriend
            .Where(uf => uf.UserID == SessionInfo.CurrentUserID) // Фильтруем друзей текущего пользователя
            .Select(uf => uf.User1.UserName) // Выбираем имена друзей из связанного объекта User
            .ToList();

            FriendList.ItemsSource = currentUserFriends; // Привязываем список имен друзей к ListView

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void UpdateUserName(object sender, SessionInfo.UserNameChangedEventArgs e)
        {
            Dispatcher.Invoke(() => {
                Tb_UserName.Text = e.NewUserName;
            });
        }

        private void Window_Closed(object sender, EventArgs e) // ??????????????????????????????????????????????????????????
        {
            SessionInfo.OnBalanceChanged -= SessionInfo_OnBalanceChanged;
        }

        private void SessionInfo_OnBalanceChanged(object sender, SessionInfo.BalanceChangedEventArgs e)
        {
            Dispatcher.Invoke(() => {
                Tb_Balance_Amount.Text = e.NewBalance.ToString();
            });
        }

        private void Btn_Change_Img(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Img.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));

                pathImage = openFileDialog.FileName;
            }
        }

        public void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // Получаем введенный текст поиска и приводим к нижнему регистру
            var filteredFriends = currentUserFriends.Where(friend => friend.ToLower().Contains(searchText)).ToList();
            FriendList.ItemsSource = filteredFriends; // Обновляем список друзей с учетом фильтра
        }


        private void Btn_Add_To_Friend(object sender, RoutedEventArgs e)
        {
            string newFriendUserName = NewFriendTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newFriendUserName))
            {
                MessageBox.Show("Введите имя пользователя");
                return;
            }

            var context = ClassHelper.EF.Context;

            // Проверяем, не пытается ли пользователь добавить самого себя
            if (newFriendUserName.Equals(SessionInfo.CurrentUserName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Вы не можете добавить себя в друзья");
                return;
            }

            // Проверяем, существует ли пользователь с таким UserName
            var userToAdd = context.User.FirstOrDefault(u => u.UserName.Equals(newFriendUserName, StringComparison.OrdinalIgnoreCase));
            if (userToAdd == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            // Проверяем, уже ли этот пользователь в друзьях
            bool alreadyFriends = context.UserFriend.Any(uf => uf.UserID == SessionInfo.CurrentUserID && uf.UserFriendID == userToAdd.ID);
            if (alreadyFriends)
            {
                MessageBox.Show("Этот пользователь уже находится у вас в друзьях!");
                return;
            }

            // Добавляем нового друга
            UserFriend newFriend = new UserFriend
            {
                UserID = SessionInfo.CurrentUserID,
                UserFriendID = userToAdd.ID
            };
            context.UserFriend.Add(newFriend);
            context.SaveChanges();

            // Обновляем список друзей в UI
            UpdateFriendList();
            MessageBox.Show("Новый друг добавлен");
        }

        private void Btn_Delete_Friend(object sender, EventArgs e)
        {
            // Получаем выбранное имя друга из ListView
            var selectedFriendUserName = FriendList.SelectedItem as string;

            if (selectedFriendUserName == null)
            {
                MessageBox.Show("Выберите друга для удаления.");
                return;
            }

            var context = ClassHelper.EF.Context;

            // Получаем ID пользователя, имя которого выбрано
            var friendToDelete = context.User.FirstOrDefault(u => u.UserName == selectedFriendUserName);

            // Находим и удаляем запись из таблицы UserFriend
            var userFriendRecord = context.UserFriend.FirstOrDefault(uf => uf.UserID == SessionInfo.CurrentUserID && uf.UserFriendID == friendToDelete.ID);
            if (userFriendRecord != null)
            {
                context.UserFriend.Remove(userFriendRecord);
                context.SaveChanges(); // Сохраняем изменения в базе данных
            }

            // Обновляем список друзей в интерфейсе
            UpdateFriendList();
        }

        private void UpdateFriendList()
        {
            currentUserFriends = ClassHelper.EF.Context.UserFriend
                .Where(uf => uf.UserID == SessionInfo.CurrentUserID)
                .Select(uf => uf.User1.UserName)
                .ToList();

            FriendList.ItemsSource = currentUserFriends;
        }

    }
}
