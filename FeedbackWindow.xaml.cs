using MayaGameLauncher.ClassHelper;
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
    /// Логика взаимодействия для FeedbackWindow.xaml
    /// </summary>
    public partial class FeedbackWindow : Window
    {
        public FeedbackWindow()
        {
            InitializeComponent();
            CmbCategory.ItemsSource = ClassHelper.EF.Context.CategoryFeedback.ToList();
            CmbCategory.DisplayMemberPath = "Title";
            CmbCategory.SelectedIndex = 0;
        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text))
            {
                MessageBox.Show("Заполните поле фидбека");
                return;
            }

            // добавление данных в бд

            DB.Feedback addFeedback = new DB.Feedback
            {
                UserID = SessionInfo.CurrentUserID,
                FeedbackDate = DateTime.Now,
                FeedbackText = UserNameTextBox.Text,
                CategoryID = (CmbCategory.SelectedItem as DB.CategoryFeedback).ID
            };

            ClassHelper.EF.Context.Feedback.Add(addFeedback);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Ваше сообщение было успешно отправлено!");
            this.Close();
        }
    }
}
