using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.kmd.Helpers {
    public static class Extensions {
        #region TimeSpan extensions

        public static string ToPrettyString (this TimeSpan ts) {
            if (ts.Days > 0) {
                return ts.ToString ("d'd 'h'h 'm'm 's's'");
            }
            if (ts.Hours > 0) {
                return ts.ToString ("h'h 'm'm 's's'");
            }
            if (ts.Minutes > 0) {
                return ts.ToString ("m'm 's's'");
            }
            else
                return ts.ToString ("s's'");
        }

        #endregion
    }
}
