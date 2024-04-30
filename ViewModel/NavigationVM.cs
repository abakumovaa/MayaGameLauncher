using MayaGameLauncher.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MayaGameLauncher.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView { get { return _currentView; } set { _currentView = value; OnPropertyChanged(); } }

        public ICommand HomeCommand { get; set; }
        public ICommand LibraryCommand { get; set; }

        public ICommand SettingsCommand { get; set; }
        public ICommand StoreCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Library(object obj) => CurrentView = new LibraryVM();
        private void Settings(object obj) => CurrentView = new SettingVM();

        private void Store(object obj) => CurrentView = new StoreVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            LibraryCommand = new RelayCommand(Library);
            SettingsCommand = new RelayCommand(Settings);
            StoreCommand = new RelayCommand(Store);

            CurrentView = new HomeVM();
        }
    }
}
