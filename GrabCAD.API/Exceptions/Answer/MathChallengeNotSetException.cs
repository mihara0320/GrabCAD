using System;

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
