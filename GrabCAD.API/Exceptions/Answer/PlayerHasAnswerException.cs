using System;

namespace GrabCAD.API.Exceptions
{
    public class PlayerHasAnswerException : Exception
    {
        public PlayerHasAnswerException()
        {
        }
        public PlayerHasAnswerException(string message) : base(message)
        {
        }
        public PlayerHasAnswerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
