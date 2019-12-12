using System;

namespace Carwings.ApiClient
{
    public class CarwingsException : Exception
    {
        internal CarwingsException(string message) : base(message)
        {
        }

        public static CarwingsException Timeout => new CarwingsException("Operation timed out.");
    }
}
