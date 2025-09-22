namespace LibraryTests;
using Program;
public class SpellTest
{
    private Spell _hechizo;
    [SetUp]
    public void Setup()
    {

         _hechizo = new Spell("bola de fuego", 200, 0, 0);
         

    }

    [Test]
    public void ConstructorTestSpellValid()
    {
        Assert.That(_hechizo.Name, Is.EqualTo("bola de fuego"));
        Assert.That(_hechizo.Damage, Is.EqualTo(200));
        Assert.That(_hechizo.Defense, Is.EqualTo( 0));
        Assert.That(_hechizo.Healing, Is.EqualTo( 0));
               
    }
    
 
}