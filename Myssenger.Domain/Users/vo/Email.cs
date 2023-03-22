using System.Net.Mail;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Users.vo;

public class Email : ValueObject
{
    private Email(MailAddress value)
    {
        Value = value;
    }


    public MailAddress Value; 
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, Email> TryCreate(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) ;
        try
        {
            var address = new MailAddress(content);
            return new Email(address);
        }
        catch (FormatException e)
        {
            return e;
        }
    }
}