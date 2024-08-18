namespace Pharmacy.Core.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(Exception? innerException = null) : base("Field to perform persistence operation", innerException)
        {
        }
    }
}
