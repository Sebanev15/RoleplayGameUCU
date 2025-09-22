using NUnit.Framework;

namespace LibraryTests;
using Program;

public class ArmorTests
{
    private Armor armor;
    
    
    [SetUp]
    public void Setup()
    {
        armor = new Armor("Malla de Espinas", 0, 30, false);
    }
    
    [Test]
    public void ConstructorValido()
    {
        Assert.That(armor.Name, Is.EqualTo("Malla de Espinas"));
        Assert.That(armor.Attack, Is.EqualTo(0));
        Assert.That(armor.Defense, Is.EqualTo(30));
        Assert.That(armor.IsMagical, Is.EqualTo(false));
        Assert.That(armor.HealValue, Is.EqualTo(0));
    }

    [Test]
    public void HealValueSiempreDeberiaSerCero()
    {
        Assert.That(armor.HealValue, Is.EqualTo(0));
    }
}