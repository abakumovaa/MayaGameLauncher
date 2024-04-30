using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaGameLauncher.ClassHelper
{
    public static class SessionInfo
    {
        public static string CurrentUserName { get; set; }
        public static int CurrentUserID { get; set; }
        //public static decimal CurrentBalance { get; set; }

            private static decimal _currentBalance;

            public static decimal CurrentBalance
            {
                get { return _currentBalance; }
                set
                {
                    if (_currentBalance != value)
                    {
                        _currentBalance = value;
                        OnBalanceChanged?.Invoke(null, new BalanceChangedEventArgs { NewBalance = _currentBalance });
                    }
                }
            }

            public static event EventHandler<BalanceChangedEventArgs> OnBalanceChanged;

            public class BalanceChangedEventArgs : EventArgs
            {
                public decimal NewBalance { get; set; }
            }
        }
}