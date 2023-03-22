using Mysennger.Domain.Chats.vo;
using Mysennger.Domain.Users.vo;
using Myssenger.Shared;

namespace Mysennger.Domain.Users;

public class User : AggregateRoot<UserId>
{
    private User(UserId id, Login login, Email email, Password password) : base(id)
    {
        Login = login;
        Email = email;
        Password = password;
    }
    
    public Login Login { get; }
    public Email Email { get; }
    public Password Password { get; }

    public static User Create(Login login, Email email, Password password)
    {
        return new User(
            UserId.CreateUnique(),
            login,
            email,
            password);
    }
}