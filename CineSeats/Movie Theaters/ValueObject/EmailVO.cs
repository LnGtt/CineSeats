namespace CineSeats.Movie_Theaters.ValueObject;

public class EmailVO
{
    public string EmailAddress { get; set; }

    public EmailVO(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new ArgumentNullException(nameof(emailAddress));
        }

        if (!emailAddress.Contains("@"))
        {
            throw new ArgumentException("Invalid email address");
        }

        if (!emailAddress.Contains("."))
        {
            throw new ArgumentException("Invalid email address");
        }
        
        this.EmailAddress = emailAddress;
    }
}