using System;

namespace AssertProperties
{
    public class AssertExpcetion : ApplicationException
    {
        public AssertExpcetion(string message)
            : base(message)
        {
        }
    }
}