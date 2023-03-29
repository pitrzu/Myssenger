using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public abstract class PrivacySettings : ValueObject
{
    public static PrivacySettings Public { get; } = new PublicSettings();
    public static PrivacySettings Private { get; } = new PrivateSettings();
    public static PrivacySettings InviteOnly { get; } = new InviteOnlySettings();
    
    public abstract bool IsPrivate { get; }
    public abstract bool IsHidden { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return IsPrivate;
        yield return IsHidden;
    }

    private class PublicSettings : PrivacySettings
    {
        public override bool IsPrivate => false;
        public override bool IsHidden => false;
    }

    private class PrivateSettings : PrivacySettings
    {
        public override bool IsPrivate => true;
        public override bool IsHidden => false;
    }

    private class InviteOnlySettings : PrivacySettings
    {
        public override bool IsPrivate => true;
        public override bool IsHidden => true;
    }
}