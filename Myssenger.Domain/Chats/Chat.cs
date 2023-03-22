using System.Collections.Immutable;
using System.Data;
using Mysennger.Domain.Chats.exceptions;
using Mysennger.Domain.Chats.vo;
using Myssenger.Shared;
using OneOf.Monads;
using OneOf.Types;

namespace Mysennger.Domain.Chats;

public sealed class Chat : AggregateRoot<ChatId>
{
    private readonly ISet<UserId> _participants;
    private readonly ISet<UserId> _bannedUsers = new HashSet<UserId>();
    private readonly ISet<Message> _messages = new HashSet<Message>();

    private Chat(
        ChatId id,
        UserId creator, 
        ISet<UserId> participants, 
        Title title) : base(id)
    {
        Creator = creator;
        Title = title;
        _participants = participants;
    }

    public UserId Creator { get; }
    public Title Title { get; set; }
    
    public IReadOnlySet<UserId> Participants
        => _participants.ToImmutableHashSet();

    public IReadOnlySet<UserId> BannedUsers
        => _participants.ToImmutableHashSet();

    public IReadOnlySet<Message> Messages
        => _messages.ToImmutableHashSet();

    public static Result<ChatCreationException, Chat> Create(
        UserId creator,
        ISet<UserId> participants,
        Title title
    )
    {
        if (participants.Count < 2)
            return new ChatCreationException(participants.Count);
        return new Chat(
            ChatId.CreateUnique(),
            creator,
            participants,
            title);
    }

    public Result<ConstraintException, UserId> AddParticipant(UserId userId)
    {
        if (_bannedUsers.Contains(userId))
            return new UserBannedException(userId, Id);
        _participants.Add(userId);
        
        return userId;
    }

    public Result<ConstraintException, UserId> RemoveParticipant(UserId userId)
    {
        if (!_participants.Contains(userId))
            return new ConstraintException($"User {userId} does not participate in chat {Id}");
        if (_participants.Count == 2)
            return new UserRemovalException(_participants.Count);
        _participants.Remove(userId);

        return userId;
    }

    public Result<ConstraintException, UserId> BanUser(UserId userId)
    {
        if (userId == Creator)
            return new ConstraintException("Chat creator can not be banned!");
        _participants.Remove(userId);
        _bannedUsers.Add(userId);

        return userId;
    }

    public void UnBanUser(UserId userId)
    {
        _bannedUsers.Remove(userId);
    }

    public Result<UserNotParticipatingException, Message> SendMessage(UserId userId, Content content)
    {
        if (!_participants.Contains(userId))
            return new UserNotParticipatingException(userId, Id);
        var message = Message.Create(userId, content);
        _messages.Add(message);

        return message;
    }

    public Result<MessageNotFoundException, MessageId> UpdateMessage(
        MessageId messageId,
        Content content)
    {
        var toUpdate = _messages.FirstOrDefault(message => message.Id == messageId);
        if (ReferenceEquals(null, toUpdate))
            return new MessageNotFoundException(messageId, Id);

        _messages.Remove(toUpdate);
        
        toUpdate.Content = content;
        _messages.Add(toUpdate);

        return messageId;
    }

    public bool RemoveMessage(MessageId messageId)
    {
        var messageToRemove = _messages.FirstOrDefault(message => message.Id == messageId);
        if (ReferenceEquals(null, messageToRemove))
            return false;

        _messages.Remove(messageToRemove);
        return true;
    }
}