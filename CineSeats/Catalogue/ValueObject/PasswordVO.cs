namespace CineSeats.Catalogue.ValueObject;

public class PasswordVO
{
    public string Password { get; set; }

    public PasswordVO(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (password.Length < 8)
        {
            throw new ArgumentOutOfRangeException(nameof(password), "Password must be at least 8 characters long");
        }
        
        if (password.Length > 20) 
        {
            throw new ArgumentOutOfRangeException(nameof(password), "Password must be less than 20 characters long");
        }

        if (!password.Any(char.IsUpper))
        {
            throw new ArgumentException("Password must contain at least one upper case letter");
        }

        if (!password.Any(char.IsDigit))
        {
            throw new ArgumentException("Password must contain at least one digit");
        }

        this.Password = password;
    }
}