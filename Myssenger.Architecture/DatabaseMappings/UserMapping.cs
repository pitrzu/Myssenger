using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users;
using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Architecture.DatabaseMappings;

public sealed class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Ignore(user => user.SubscribedTo);
        builder.Ignore(user => user.Follows);
        builder.Ignore(user => user.FollowedBy);
        
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasColumnName("user_id")
            .HasColumnOrder(0)
            .HasConversion(obj => obj.Value,
                value => UserId.Create(value));
        builder.Property(user => user.Name)
            .HasColumnName("user_name")
            .HasConversion(obj => obj.Value,
                value => UserName.CreateOrThrow(value));
        builder.Property(user => user.Login)
            .HasColumnName("user_login")
            .HasConversion(obj => obj.Value,
                value => Login.CreateOrThrow(value));
        builder.Property(user => user.Password)
            .HasColumnName("user_password")
            .HasConversion(obj => obj.Hash,
                value => Password.Create(value));
        builder.OwnsMany<SubriddotId>("_subscribedTo", subscriberBuilder =>
        {
            subscriberBuilder.ToTable("user_subscriptions");
            
            subscriberBuilder.Property(id => id.Value)
                .HasColumnName("subriddot_id");
        });
        builder.OwnsMany<UserId>("_follows", followsBuilder =>
        {
            followsBuilder.ToTable("user_follows");

            followsBuilder.Property(id => id.Value)
                .HasColumnName("followed_id");
        });
        builder.OwnsMany<UserId>("_followedBy", followedByBuilder =>
        {
            followedByBuilder.ToTable("user_followed_by");

            followedByBuilder.Property(id => id.Value)
                .HasColumnName("followed_by_id");
        });
    }
}