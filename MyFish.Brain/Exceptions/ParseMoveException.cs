using System;

namespace MyFish.Brain.Exceptions
{
    public class ParseMoveException : Exception
    {
        public ParseMoveException(string message) : base(message)
        {            
        }
    }
}