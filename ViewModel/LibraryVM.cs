using MayaGameLauncher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaGameLauncher.ViewModel
{
    class LibraryVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public int GameCount
        {
            get { return _pageModel.GameCount; }
            set { _pageModel.GameCount = value; OnPropertyChanged(); }
        }

        public LibraryVM()
        {
            _pageModel = new PageModel();
            GameCount = 5;
        }

    }
}
