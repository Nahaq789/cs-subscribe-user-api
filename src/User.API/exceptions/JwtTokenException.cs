namespace User.API.exceptions;

public class JwtTokenException : Exception
{
    public JwtTokenException() { }
    public JwtTokenException(string message) : base(message) { }
    public JwtTokenException(string message, Exception innerException) : base(message, innerException) { }
}