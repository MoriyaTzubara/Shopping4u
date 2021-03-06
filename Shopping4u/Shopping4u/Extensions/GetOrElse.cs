using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Extensions
{
    static class GetOrElse
    {
        public static object getOrElse(this object val, object fallback)
        {
            if (val == null)
                return fallback;
            return val;
        }        
        public static T getOrElse<T>(this T val, T fallback)
        {
            if (val == null)
                return fallback;
            return val;
        }
        public static object getOrElse(this bool val, object fallback)
        {
            if (!val)
                return fallback;
            return val;
        }
    }
}
