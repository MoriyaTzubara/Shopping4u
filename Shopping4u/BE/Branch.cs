using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Branch
    {
        public int id;
        public string name;

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
