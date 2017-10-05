using System;

namespace vc.service
{
    public class VacationException : Exception
    {
        public VacationException(string message = "") : base(message)
        {
        }
    }
}