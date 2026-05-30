using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Domain.Entities;

public class Admin
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public EmailVO EmailAddress { get; private set; }
    public PasswordVO Password { get; private set; }

    public Admin(string name, EmailVO email, PasswordVO password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty");
        
        if (email == null)
            throw new ArgumentException("Email cannot be null");
        
        if (password == null)
            throw new ArgumentException("Password cannot be null");
        
        Name = name;
        EmailAddress = email;
        Password = password;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty");
        
        Name = name;
    }

    /*public void ChangeEmail(EmailVO emailAddress) //Ver como implementar verificação de email e troca partir de email
    {
        
    }

    public void ChangePassword(PasswordVO password)
    {
        
    }*/
}