using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Consumer
    {
        public int id;
        public string password;
        public string firstName;
        public string lastName;
        public string phoneNumber;
        public string email;  // It is also the username
        public string profileImageUrl;

        public string fullName() { return $"{firstName} {lastName}"; }
    }
}
