namespace LibraryTests;
using Program;

public class SpellBookTest
{
    private SpellBook _libro;
    private Spell _hechizo;

    [SetUp]
    public void Setup()
    {

        _libro = new SpellBook("Necronomicon");
        _hechizo = new Spell("bola de fuego magica", 200, 1, 1);

    }

    [Test]
    public void ConstructorTestSpellValid()
    {
        Assert.That(_libro.Name, Is.EqualTo("Necronomicon"));
        Assert.That(_libro.Attack, Is.EqualTo(0));
        Assert.That(_libro.Defense, Is.EqualTo(0));
        Assert.That(_libro.HealValue, Is.EqualTo(0));
        Assert.That(_libro.IsMagical, Is.EqualTo(true));
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));
    }

    [Test]
    public void ConstructorTestSpellValidAfterSpell()
    {
        Assert.That(_libro.Name, Is.EqualTo("Necronomicon"));
        Assert.That(_libro.Attack, Is.EqualTo(0));
        Assert.That(_libro.Defense, Is.EqualTo(0));
        Assert.That(_libro.HealValue, Is.EqualTo(0));
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));

        _libro.AddSpell(_hechizo);

        Assert.That(_libro.Name, Is.EqualTo("Necronomicon"));
        Assert.That(_libro.Attack, Is.EqualTo(200));
        Assert.That(_libro.Defense, Is.EqualTo(1));
        Assert.That(_libro.HealValue, Is.EqualTo(1));
        Assert.That(_libro.Spells.Count(), Is.EqualTo(1));
        Assert.That(_libro.IsMagical, Is.EqualTo(true));
    }

    [Test]
    public void AddingSameSpellTwice()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));
        _libro.AddSpell(_hechizo);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(1));
        _libro.AddSpell(_hechizo);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(1));

    }
    
    [Test]
    public void RemovingSameSpellTwice()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));
        _libro.AddSpell(_hechizo);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(1));
        _libro.RemoveSpell(_hechizo);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));
        _libro.RemoveSpell(_hechizo);
        Assert.That(_libro.Spells.Count(), Is.EqualTo(0));

    }
}
    
 
