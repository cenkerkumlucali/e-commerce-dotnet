namespace ECommerce.Application.Exceptions;

public class UserCreateFailedException:Exception
{
    public UserCreateFailedException(string message) : base(message)
    {
    }

    public UserCreateFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public UserCreateFailedException():base("Kullanıcı oluşturulurken beklenmeyen bir hata ile karşılaşıldı.")
    {
        
    }
}