using BE;
using Shopping4u.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping4u.Models
{
    public class SignInModel
    {
        public bool SignIn(string userName, string password)
        {
            IBL bl = new BL.BL();
            return false;
            // TODO //
            //return bl.SignIn(userName, password);
        }

        public Consumer GetConsumer(string userName)
        {
            IBL bl = new BL.BL();
            //bl.GetConsumer(userName);
            // TODO //
            return new Consumer();
        }
    }
}
