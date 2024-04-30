using MayaGameLauncher.ClassHelper;
using MayaGameLauncher.DB;
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

namespace MayaGameLauncher.View
{
    /// <summary>
    /// Логика взаимодействия для Store.xaml
    /// </summary>
    public partial class Store : UserControl
    {
        public Store()
        {
            InitializeComponent();
            GetListService();
        }

        private void GetListService()
        {
            LvStore.ItemsSource = ClassHelper.EF.Context.Game.ToList();
        }

        private void LvStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_Buy(object sender, RoutedEventArgs e)
        {
            var selectedGame = LvStore.SelectedItem as Game;
            var user = ClassHelper.EF.Context.User.FirstOrDefault(u => u.ID == SessionInfo.CurrentUserID);
            if (selectedGame == null)
            {
                MessageBox.Show("Выберите игру!");
                return;
            }

            // Проверка, куплена ли уже игра
            bool isPurchased = ClassHelper.EF.Context.FavoriteGame.Any(fg => fg.GameID == selectedGame.ID && fg.UserID == SessionInfo.CurrentUserID);
            if (isPurchased)
            {
                MessageBox.Show("Эта игра уже куплена!");
                return;
            }

            // Проверка достаточности средств
            decimal price = selectedGame.Price ?? 0m;  // Если Price равно null, используем 0

            if (SessionInfo.CurrentBalance >= price)
            {
                user.UserBalance = user.UserBalance - price;
                SessionInfo.CurrentBalance -= price; // Это вызовет событие
                MessageBox.Show("Игра была успешно куплена!");


                // Добавление игры в избранное
                var newFavorite = new FavoriteGame
                {
                    UserID = SessionInfo.CurrentUserID,
                    GameID = selectedGame.ID
                };
                ClassHelper.EF.Context.FavoriteGame.Add(newFavorite);
                ClassHelper.EF.Context.SaveChanges();

                MessageBox.Show("Игра была успешно куплена!");

                // Обновление списка избранных игр, если это необходимо
                if (Application.Current.Windows.OfType<Library>().Any())
                {
                    var libraryWindow = Application.Current.Windows.OfType<Library>().FirstOrDefault();
                    libraryWindow.LoadFavoriteGames();
                }
            }
            else
            {
                MessageBox.Show("Недостаточно средств для покупки!");
            }
        }
    }
}
