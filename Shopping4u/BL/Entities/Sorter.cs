using BL.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL.Entities
{
    [Export(typeof(ISorter))]
    class Sorter : ISorter
    {
        string ISorter.Sort(string token)
        {
            string[] tokenArray = token.Split(',');
            Array.Sort(tokenArray);
            return String.Join(",", tokenArray);
        }
    }
}
