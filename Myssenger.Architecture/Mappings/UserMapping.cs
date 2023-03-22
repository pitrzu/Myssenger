using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mysennger.Domain.Chats.vo;
using Mysennger.Domain.Users;
using Mysennger.Domain.Users.vo;

namespace Mysennger.Architecture.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Ignore(user => user.Events);
        
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Id)
            .HasColumnName("user_id")
            .ValueGeneratedNever()
            .HasConversion(obj => obj.Value,
                value => UserId.Create(value));
        builder.Property(user => user.Login)
            .HasColumnName("user_login")
            .HasConversion(obj => obj.Value,
                value => Login.TryCreate(value).SuccessValue());
        builder.Property(user => user.Email)
            .HasColumnName("user_email")
            .HasConversion(obj => obj.Value.ToString(),
                value => Email.TryCreate(value).SuccessValue());
        builder.OwnsOne(user => user.Password, passwordBuilder =>
        {
            passwordBuilder.Property(password => password.Hash)
                .HasColumnName("user_password_hash");
            passwordBuilder.Property(password => password.Salt)
                .HasColumnName("user_password_salt");
        });
    }
}