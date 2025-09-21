namespace LibraryTests;
using Program;

public class HeallingTests
{
    private Healing healing;
    
    
    [SetUp]
    public void Setup()
    {
        healing = new Healing("Poción de Vida", 5, true, 20);
    }

    [Test]
    public void ConstructorValido()
    {
        Assert.That(healing.Name, Is.EqualTo("Poción de Vida"));
        Assert.That(healing.Attack, Is.EqualTo(0));
        Assert.That(healing.Defense, Is.EqualTo(5));
        Assert.IsTrue(healing.IsMagical);
        Assert.That(healing.HealValue, Is.EqualTo(20));
    }

    [Test]
    public void AttackSiempreDeberiaSerCero()
    {
        Assert.That(healing.Attack, Is.EqualTo(0));
    }
}
