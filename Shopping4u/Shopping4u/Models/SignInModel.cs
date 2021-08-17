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
        public bool SignIn(string email, string password)
        {
            IBL bl = new BL.BL();
            // TODO //
            return bl.SignIn(email, password);
        }

        public Consumer GetConsumer(string email)
        {
            IBL bl = new BL.BL();
            return bl.GetConsumer(email);
            // TODO //
            //return new Consumer();
        }
    }
}
