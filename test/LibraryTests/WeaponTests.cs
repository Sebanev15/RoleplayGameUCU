namespace LibraryTests;
using Program;

public class WeaponTests
{
    private Weapon weapon;
    
    
    [SetUp]
    public void Setup()
    {
        weapon = new Weapon("Arco", 15, 0, false);
    }
    
    [Test]
    public void ConstructorValido()
    {
        Assert.That(weapon.Name, Is.EqualTo("Arco"));
        Assert.That(weapon.Attack, Is.EqualTo(15));
        Assert.That(weapon.Defense, Is.EqualTo(0));
        Assert.IsFalse(weapon.IsMagical);
        Assert.That(weapon.HealValue, Is.EqualTo(0));
    }

    [Test]
    public void HealValueSiempreDeberiaSerCero()
    {
        Assert.That(weapon.HealValue, Is.EqualTo(0));
    }
}