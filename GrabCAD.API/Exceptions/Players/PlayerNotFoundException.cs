using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Exceptions
{
    public class PlayersExceedLimitException : Exception
    {
        public PlayersExceedLimitException()
        {
        }
        public PlayersExceedLimitException(string message) : base(message)
        {
        }
        public PlayersExceedLimitException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
