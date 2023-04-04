using Myssenger.Shared;

namespace Myssenger.Tests.Shared.ValueObjectTests;

internal class ValueObj : ValueObject
{
    public ValueObj(int value)
    {
        Value = value;
    }
    
    public int Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}

public sealed class BasicValueObjectTests
{
    [Test]
    public void ValueMakesEqual()
    {
        var voOne = new ValueObj(5);
        var voTwo = new ValueObj(5);

        Assert.That(voTwo, Is.EqualTo(voOne));
    }

    [Test]
    public void ValueMakesNotEqual()
    {
        var voOne = new ValueObj(5);
        var voTwo = new ValueObj(6);
        
        Assert.That(voTwo, Is.Not.EqualTo(voOne));
    }
}