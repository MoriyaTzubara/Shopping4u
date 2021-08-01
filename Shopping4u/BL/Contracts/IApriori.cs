using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Entities;
namespace BL.Contracts
{
        using System.Collections.Generic;

        public interface IApriori
        {
            Output ProcessTransaction(double minSupport, double minConfidence, IEnumerable<string> items, string[] transactions);
        }
}
