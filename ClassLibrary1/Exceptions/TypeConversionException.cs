namespace MockarooLibrary.Exceptions
{
    public class TypeConversionException : Exception
    {
        public TypeConversionException() : base()
        {
        }

        public TypeConversionException(string message) : base(message)
        {
        }

        public TypeConversionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}