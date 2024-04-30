using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayaGameLauncher.Model;
using MayaGameLauncher.Utilities;
using System.ComponentModel;

namespace MayaGameLauncher.ViewModel
{
    class SettingVM : Utilities.ViewModelBase
    {
        private MediaPlayerService _mediaPlayerService;

        public SettingVM()
        {
            _mediaPlayerService = MediaPlayerService.Instance;
            Volume = _mediaPlayerService.Volume;
        }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
                _mediaPlayerService.Volume = _volume;
            }
        }
    }
}

