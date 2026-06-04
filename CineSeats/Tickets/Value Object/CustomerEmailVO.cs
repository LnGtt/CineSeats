namespace CineSeats.Tickets.Value_Object;

public class CustomerEmailVO
{
    public string EmailAddress { get; private set; }
    public CustomerEmailVO(string emailAddress)
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