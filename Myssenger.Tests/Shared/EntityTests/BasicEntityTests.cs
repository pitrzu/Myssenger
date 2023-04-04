using Myssenger.Shared;

namespace Myssenger.Tests.Shared.EntityTests;

internal class BasicEntityId : ValueObject
{
    public BasicEntityId(int value)
    {
        Value = value;
    }

    public int Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}

internal class BasicEntity : Entity<BasicEntityId>
{
    public string Property { get; set; }

    public BasicEntity(BasicEntityId id, string property) : base(id)
    {
        Property = property;
    }
}

public sealed class BasicEntityTests
{
    [Test]
    public void SameIdShouldMakeEqual()
    {
        var id = new BasicEntityId(0);

        var eOne = new BasicEntity(id, "a");
        var eTwo = new BasicEntity(id, "b");
        
        Assert.That(eOne, Is.EqualTo(eTwo));
    }

    [Test]
    public void EqualIdShouldMakeEqual()
    {
        var id1 = new BasicEntityId(0);
        var id2 = new BasicEntityId(0);

        var eOne = new BasicEntity(id1, "a");
        var eTwo = new BasicEntity(id2, "b");
        
        Assert.That(eOne, Is.EqualTo(eTwo));
    }

    [Test]
    public void DifferentIdShouldNotMakeEqual()
    {
        var id1 = new BasicEntityId(0);
        var id2 = new BasicEntityId(1);

        var eOne = new BasicEntity(id1, "");
        var eTwo = new BasicEntity(id2, "");
        
        Assert.That(eOne, Is.Not.EqualTo(eTwo));
    }
}