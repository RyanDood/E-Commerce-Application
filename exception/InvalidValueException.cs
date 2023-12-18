using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.exception
{
    internal class InvalidValueException:ApplicationException
    {
        public InvalidValueException(string msg) : base(msg)
        {

        }
    }
}
