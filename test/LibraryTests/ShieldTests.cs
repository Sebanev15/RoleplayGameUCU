using NUnit.Framework;

namespace LibraryTests;
using Program;

public class ShieldTests
{
    private Shield shield;
    
    
    [SetUp]
    public void Setup()
    {
        shield = new Shield("Escudo de Doran", 0, 20, false);
    }
    
    [Test]
    public void ConstructorValido()
    {
        Assert.That(shield.Name, Is.EqualTo("Escudo de Doran"));
        Assert.That(shield.Attack, Is.EqualTo(0));
        Assert.That(shield.Defense, Is.EqualTo(20));
        Assert.That(shield.IsMagical, Is.EqualTo(false));
        Assert.That(shield.HealValue, Is.EqualTo(0));
    }

    [Test]
    public void HealValueSiempreDeberiaSerCero()
    {
        Assert.That(shield.HealValue, Is.EqualTo(0));
    }
}