using MayaGameLauncher.Model;
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
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Library()
        {
            InitializeComponent();
            LoadFavoriteGames();
        }

        public void LoadFavoriteGames()
        {
            var favoriteGamesList = GetCurrentUserFavoriteGames();
            LvFavorite.ItemsSource = favoriteGamesList; // Привязка данных к ListView
        }

        private List<GameModel> GetCurrentUserFavoriteGames()
        {
            return ClassHelper.EF.Context.FavoriteGame
                .Where(fg => fg.UserID == ClassHelper.SessionInfo.CurrentUserID)
            .Select(fg => fg.Game)
                .Select(game => new GameModel
                {
                    Title = game.Title,
                    Description = game.Description,
                    PcRequirements = game.PcRequirements,
                    GamePoster = game.GamePoster
                })
                .ToList();
        }

        private void LvFavorite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_Download(object sender, EventArgs e)
        {
            MessageBox.Show("Скачивание игры...");
        }

    }
}
