namespace Pharmacy.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Exception? innerException = null) : base("Resource not found", innerException)
        {
        }
    }
}
