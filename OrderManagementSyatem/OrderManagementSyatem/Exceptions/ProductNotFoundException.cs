using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSyatem.Exceptions
{
    internal class ProductNotFoundException:Exception
    {
        public ProductNotFoundException() : base("Product id is invalid")
        { 

        }

    }
}
