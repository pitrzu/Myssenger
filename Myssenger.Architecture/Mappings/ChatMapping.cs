using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mysennger.Domain.Chats;
using Mysennger.Domain.Chats.vo;

namespace Mysennger.Architecture.Mappings;

public class ChatMapping : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.Ignore(chat => chat.Events);
        builder.Ignore(chat => chat.Messages);
        builder.Ignore(chat => chat.Participants);
        builder.Ignore(chat => chat.BannedUsers);
        
        builder.ToTable("chats");
        builder.HasKey(chat => chat.Id);
        builder.Property(chat => chat.Id)
            .HasColumnName("chat_id")
            .ValueGeneratedNever()
            .HasConversion(obj => obj.Value,
                value => ChatId.Create(value));

        builder.Property(chat => chat.Creator)
            .HasColumnName("chat_creator");
        builder.Property(chat => chat.Title)
            .HasColumnName("chat_title")
            .HasConversion(obj => obj.Value,
                value => Title.TryCreate(value).SuccessValue());
        
        builder.OwnsMany<UserId>("_participants", participantsBuilder =>
        {
            participantsBuilder.ToTable("chat_participants");
            participantsBuilder.Property(id => id.Value)
                .HasColumnName("participant_id");
        });
        
        builder.OwnsMany<UserId>("_bannedUsers", bannedUsersBuilder =>
        {
            bannedUsersBuilder.ToTable("chat_banned_users");
            bannedUsersBuilder.Property(id => id.Value)
                .HasColumnName("banned_user_id");
        });
        
        builder.OwnsMany<Message>("_messages", messagesBuilder =>
        {
            messagesBuilder.ToTable("chat_messages");
            messagesBuilder.HasKey(message => message.Id);
            messagesBuilder.Property(message => message.Id)
                .HasColumnName("message_id")
                .ValueGeneratedNever()
                .HasConversion(obj => obj.Value,
                    value => MessageId.Create(value));
            messagesBuilder.Property(message => message.Sender)
                .HasColumnName("message_sender")
                .HasConversion(obj => obj.Value,
                    value => UserId.Create(value));
            messagesBuilder.Property(message => message.Content)
                .HasColumnName("message_content")
                .HasConversion(obj => obj.Value,
                    value => Content.TryCreate(value).SuccessValue());
        });
    }
}