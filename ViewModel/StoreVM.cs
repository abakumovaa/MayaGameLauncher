using MayaGameLauncher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaGameLauncher.ViewModel
{
    class StoreVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string GameName
        {
            get { return _pageModel.GameName; }
            set { _pageModel.GameName = value; OnPropertyChanged(); }
        }

        public StoreVM()
        {
            _pageModel = new PageModel();
            GameName = "Экзодия: Возрождение";
        }
    }
}
