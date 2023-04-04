using Myssenger.Shared;

namespace Myssenger.Tests.Shared.ValueObjectTests;

internal class TwoValueObject : ValueObject
{
    public TwoValueObject(int equatable, int notEquatable)
    {
        Equatable = equatable;
        NotEquatable = notEquatable;
    }
    
    public int Equatable { get; }
    public int NotEquatable { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Equatable;
    }
}

public sealed class TwoValueObjectTests
{
    [Test] 
    public void TwoValuesShouldMakeEqual()
    {
        var voOne = new TwoValueObject(5, 1);
        var voTwo = new TwoValueObject(5, 1);
        
        Assert.That(voOne, Is.EqualTo(voTwo));
    }

    [Test]
    public void FirstValueShouldMakeEqual()
    {
        var voOne = new TwoValueObject(5, 1);
        var voTwo = new TwoValueObject(5, 2);
        
        Assert.That(voOne, Is.EqualTo(voTwo));
    }

    [Test]
    public void SecondValueShouldNotMakeEqual()
    {
        var voOne = new TwoValueObject(1, 5);
        var voTwo = new TwoValueObject(2, 5);
        
        Assert.That(voOne, Is.Not.EqualTo(voTwo));
    }

    [Test]
    public void DifferentValuesShouldNotMakeEqual()
    {
        var voOne = new TwoValueObject(1, 2);
        var voTwo = new TwoValueObject(3, 4);
        
        Assert.That(voOne, Is.Not.EqualTo(voTwo));
    }
}