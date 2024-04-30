using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaGameLauncher.ClassHelper
{
    public static class SessionInfo
    {
        public static int CurrentUserID { get; set; }

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

        private static string _currentUserName;
        public static string CurrentUserName
        {
            get { return _currentUserName; }
            set
            {
                if (_currentUserName != value)
                {
                    _currentUserName = value;
                    OnUserNameChanged?.Invoke(null, new UserNameChangedEventArgs { NewUserName = _currentUserName });
                }
            }
        }

        public static event EventHandler<UserNameChangedEventArgs> OnUserNameChanged;

        public class UserNameChangedEventArgs : EventArgs
        {
            public string NewUserName { get; set; }
        }



    }
}