namespace ECommerce.Application.Exceptions;

public class PasswordConfirmFailedException : Exception
{
    public PasswordConfirmFailedException() : base("Şifreler uyuşmuyor.")
    {
    }

    public PasswordConfirmFailedException(string message) : base(message)
    {
    }

    public PasswordConfirmFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}