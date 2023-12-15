using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Exception
{
    internal class CartNotFoundException:ApplicationException
    {
        public CartNotFoundException(string message) : base(message)
        {

        }
    }
}
