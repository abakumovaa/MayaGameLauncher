﻿using System;
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

        private void Btn_Buy(object sender, EventArgs e)
        {
            
        }
    }
}
