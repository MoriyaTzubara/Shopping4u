using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Extensions
{
    static class GetOrElse
    {
        public static object getOrElae(this object val, object fallback)
        {
            if (val == null)
                return fallback;
            return val;
        }
    }
}
