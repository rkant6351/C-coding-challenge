using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSyatem.Exceptions
{
    internal class UserNotFoundException:Exception
    {
        public UserNotFoundException() : base("User Id is invalid")
        {

        }
    }
}
