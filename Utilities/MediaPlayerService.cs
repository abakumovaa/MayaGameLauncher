using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MayaGameLauncher.Utilities
{
    public class MediaPlayerService
    {
        private static Lazy<MediaPlayerService> _instance = new Lazy<MediaPlayerService>(() => new MediaPlayerService());
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        private MediaPlayerService()
        {
            _mediaPlayer.Open(new Uri("Sources/soundtrack.mp3", UriKind.Relative));
            _mediaPlayer.MediaEnded += (s, e) =>
            {
                _mediaPlayer.Position = TimeSpan.Zero;
                _mediaPlayer.Play();
            };
        }

        public static MediaPlayerService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public double Volume
        {
            get { return _mediaPlayer.Volume * 100; }
            set { _mediaPlayer.Volume = value / 100; }
        }

        public void Play()
        {
            _mediaPlayer.Play();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Close(); // Освобождение ресурсов
        }
    }
}
