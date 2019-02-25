using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException()
        {
        }
        public PlayerNotFoundException(string message) : base(message)
        {
        }
        public PlayerNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
