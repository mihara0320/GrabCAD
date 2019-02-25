using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Exceptions
{
    public class MathChallengeNotSetException : Exception
    {
        public MathChallengeNotSetException()
        {
        }
        public MathChallengeNotSetException(string message) : base(message)
        {
        }
        public MathChallengeNotSetException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
