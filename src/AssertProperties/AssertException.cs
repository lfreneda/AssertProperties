using System;

namespace AssertProperties
{
    public class AssertException : ApplicationException
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}