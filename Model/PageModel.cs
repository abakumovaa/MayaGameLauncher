using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaGameLauncher.Model
{
    public class PageModel
    {
        public int GameCount { get; set; } //для Library
        public string GameName { get; set; } //для Store
        public decimal GamePrice { get; set; }//для Store
        public bool LocationStatus { get; set; }

    }
}
