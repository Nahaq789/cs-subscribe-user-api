namespace User.Domain.exceptions;

public class UserDomainException : Exception
{
    public UserDomainException() : base() { }
    public UserDomainException(string message) : base(message) { }
    public UserDomainException(string message, Exception innerException) : base(message, innerException) { }
}