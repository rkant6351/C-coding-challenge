using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSyatem.Exceptions
{
    internal class OrderDoesNotExistException:Exception
    {
        public OrderDoesNotExistException() : base("order id is invalid")
        {

        } 
    }
}
