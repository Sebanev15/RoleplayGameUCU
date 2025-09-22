namespace LibraryTests;
using Program;
public class WizardTest
{
    private Wizard _mago;
    private Weapon _baston;
    private Armor _helmet;
    private Healing _bandage;
    private Healing _oldBandage;
    private string _nameEsperado = "gandalf";
    private double _healthEsperado = 100;
    private double _defenseEsperado = 20;
    private int _damageEsperado = 100;
    [SetUp]
    
    public void SetUp()
    {
        _baston = new Weapon("baston magico", 100, 0, true);
        _mago = new Wizard(_nameEsperado, _healthEsperado, _defenseEsperado, _damageEsperado);
        _helmet = new Armor("casco", 0, 10, false);
        _bandage = new Healing("bandage", 0, false, 100);
        _oldBandage = new Healing("bandage", 0, false, 1);
    }

    [Test]
    public void ConstructorTestWizardEqualValid()
    {
        Assert.That(_mago.Name, Is.EqualTo(_nameEsperado));
        Assert.That(_mago.Damage, Is.EqualTo(_damageEsperado));
        Assert.That(_mago.Health, Is.EqualTo(_healthEsperado));
        Assert.That(_mago.MaxHealth, Is.EqualTo(_healthEsperado));
        Assert.That(_mago.Defense, Is.EqualTo( _defenseEsperado));
        Assert.That(_mago.IsAlive, Is.EqualTo(true));
    }
    
    [Test]
    public void ConstructorTestWizardEqualNotValid()
    {
        Assert.That(_mago.Name, Is.Not.EqualTo("_nameEsperado"));
        Assert.That(_mago.Damage, Is.Not.EqualTo(2));
        Assert.That(_mago.Health, Is.Not.EqualTo(31));
        Assert.That(_mago.Defense, Is.Not.EqualTo( 41332));
        
    }

    [Test]
    public void AddWeaponValidWizard()
    {
        _mago.AddItem(_baston);
        Assert.IsTrue(_mago.Items.Count>0);
        Assert.That(_mago.Items[0], Is.EqualTo(_baston));
        Assert.IsFalse(_mago.Items.Count<0);
    }
    
    [Test]
    public void NotDuplicateWeaponWizard()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        _mago.AddItem(_baston);
        Assert.IsTrue(_mago.Items.Count==1);
        _mago.AddItem(_baston);
        Assert.IsTrue(_mago.Items.Count==1);
    }
    
    [Test]
    public void RemoveItemTwiceNotExceptionWizard()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        _mago.AddItem(_baston);
        Assert.IsTrue(_mago.Items.Count==1);
        _mago.RemoveItem(_baston);
        Assert.IsTrue(_mago.Items.Count==0);
        _mago.RemoveItem(_baston);
        Assert.IsTrue(_mago.Items.Count==0);
    }
    [Test]
    public void AttackItselfNoExceptionWizard()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        Assert.IsTrue(_mago.IsAlive);
        _mago.Attack(_mago);
        _mago.Attack(_mago);
        Assert.IsFalse(_mago.IsAlive);
    }

    [Test]
    
    public void GetTotalDefenseAddingItemWizard()
    {
        Assert.That(_mago.GetTotalDefense(), Is.EqualTo(20));
        _mago.AddItem(_helmet);
        Assert.That(_mago.GetTotalDefense(), Is.EqualTo(30));
    }
    
    
    [Test]
    public void GetTotalDamageAddingItemWizard()
    {
        Assert.That(_mago.GetTotalDamage(), Is.EqualTo(100));
        _mago.AddItem(_baston);
        Assert.That(_mago.GetTotalDamage(), Is.EqualTo(300));
    }
    
    [Test]
    public void GetTotalHealWithMaxHpWizard()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        _mago.AddItem(_bandage);
        _mago.Attack(_mago);
        _mago.Heal(_mago);
        Assert.IsTrue(_mago.Health==100);
    }

    [Test]
    public void HealItselfIsAliveFalseWizard()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        _mago.AddItem(_baston);
        _mago.AddItem(_bandage);
        _mago.Attack(_mago);
        _mago.Attack(_mago);
        _mago.Heal(_mago);
        Assert.IsFalse(_mago.Health>0);
        
    }

    
    [Test]
    public void HealWizardTest()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        _mago.AddItem(_oldBandage);
        _mago.Attack(_mago);
        Console.WriteLine(_mago.Health);
        _mago.Heal(_mago);
        Assert.That(_mago.Health, Is.EqualTo(16));
        
    }





}