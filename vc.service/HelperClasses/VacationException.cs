using System;

namespace vc.service.HelperClasses
{
    public class VacationException : Exception
    {
        public VacationException(string message = "Incorrect incoming data") : base(message)
        {
        }
    }
}