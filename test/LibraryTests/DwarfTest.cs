namespace LibraryTests;
using Program;
using System.IO;

public class DwarfTestsAdditional
{
    // Mock class para testing
    private class MockCharacter : ICharacter
    {
        public string Name { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; }
        public List<IItem> Items { get; }
        public double Defense { get; set; }
        public int Damage { get; set; }
        public bool IsAlive { get; set; }

        public MockCharacter(string name, double health, double defense, int damage)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Defense = defense;
            Damage = damage;
            IsAlive = true;
            Items = new List<IItem>();
        }

        public void Attack(ICharacter target) { }
        public void Heal(ICharacter target) { }
        public void AddItem(IItem item) { }
        public void RemoveItem(IItem item) { }

        public int GetTotalDamage()
        {
            return Damage;
        }

        public double GetTotalDefense()
        {
            return Defense;
        }

        public int GetTotalHeal()
        {
            return 0;
        } 
    }

    private class MockItem : IItem
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsMagical { get; set; }
        public int HealValue { get; set; }

        public MockItem(string name, int attack = 0, int defense = 0, int healValue = 0)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            HealValue = healValue;
            IsMagical = false;
        }
    }

    [Test]
    public void DwarfVerifyGetters()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 50, 20);
        Assert.That(enano.Name, Is.EqualTo("Enano Prueba"));
        Assert.That(enano.Health, Is.EqualTo(100));
        Assert.That(enano.Defense, Is.EqualTo(50));
        Assert.That(enano.Damage, Is.EqualTo(20));
        Assert.True(enano.IsAlive);
        Assert.IsEmpty(enano.Items);
    }
    
    [Test]
    public void TryToAttackAnAliveCharacterWithoutWeapon()
    {
        Dwarf enano = new Dwarf("Enano Atacante", 100, 10, 30);
        MockCharacter objetivo = new MockCharacter("Objetivo", 100, 10, 15);
        double healthInicial = objetivo.Health;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Attack(objetivo);

        double expectedDamage = 30 - (10 * 0.75); // 22.5
        Assert.That(objetivo.Health, Is.EqualTo(healthInicial - expectedDamage));
        Assert.That(sw.ToString(), Does.Contain("Se ha infligido un total de"));
    }

    [Test]
    public void TryToAttackAnAliveCharacterWithWeapon()
    {
        Dwarf enano = new Dwarf("Enano Atacante", 100, 10, 20);
        Weapon arma = new Weapon("Espada", 15, 5, false);
        enano.AddItem(arma);
        
        MockCharacter objetivo = new MockCharacter("Objetivo", 100, 10, 15);
        double healthInicial = objetivo.Health;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Attack(objetivo);

        // Daño total: 20 base + (15 * 2) weapon = 50
        // Daño aplicado: 50 - (10 * 0.75) = 42.5
        double expectedDamage = 50 - (10 * 0.75);
        Assert.That(objetivo.Health, Is.EqualTo(healthInicial - expectedDamage));
    }

    [Test]
    public void TryToAttackADeadCharacter()
    {
        Dwarf enano = new Dwarf("Enano Atacante", 100, 10, 30);
        MockCharacter objetivo = new MockCharacter("Objetivo", 0, 10, 15);
        objetivo.IsAlive = false;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Attack(objetivo);

        Assert.That(sw.ToString(), Does.Contain("ERROR"));
        Assert.That(sw.ToString(), Does.Contain("ya esta muerto"));
    }

    [Test]
    public void TryToAttackWhenDamageIsLessThanDefense()
    {
        Dwarf enano = new Dwarf("Enano Atacante", 100, 10, 5);
        MockCharacter objetivo = new MockCharacter("Objetivo", 100, 20, 15);
        double healthInicial = objetivo.Health;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Attack(objetivo);

        Assert.That(objetivo.Health, Is.EqualTo(healthInicial));
        Assert.That(sw.ToString(), Does.Contain("Se ha infligido un total de 0"));
    }

    [Test]
    public void TryToKillCharacterWithAttack()
    {
        Dwarf enano = new Dwarf("Enano Atacante", 100, 10, 100);
        MockCharacter objetivo = new MockCharacter("Objetivo", 50, 5, 15);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Attack(objetivo);

        Assert.That(objetivo.Health, Is.LessThanOrEqualTo(0));
        Assert.IsFalse(objetivo.IsAlive);
        Assert.That(sw.ToString(), Does.Contain("ha sido asesinado"));
    }

    [Test]
    public void TryToHealAnAliveCharacterWithoutItems()
    {
        Dwarf enano = new Dwarf("Enano Sanador", 100, 10, 20);
        MockCharacter objetivo = new MockCharacter("Objetivo", 50, 10, 15);
        double healthInicial = objetivo.Health;

        enano.Heal(objetivo);

        // Sin items de curación, no debe cambiar la vida
        Assert.That(objetivo.Health, Is.EqualTo(healthInicial));
    }

    [Test]
    public void TryToHealAnAliveCharacterWithHealingItem()
    {
        Dwarf enano = new Dwarf("Enano Sanador", 100, 10, 20);
        MockItem pocion = new MockItem("Poción", 0, 0, 25);
        enano.AddItem(pocion);
        
        MockCharacter objetivo = new MockCharacter("Objetivo", 50, 10, 15);
        objetivo.Health -= 25;

        enano.Heal(objetivo);

        Assert.That(objetivo.Health, Is.EqualTo(50)); // 50 + 25
    }

    [Test]
    public void TryToHealWhenHealExceedsMaxHealth()
    {
        Dwarf enano = new Dwarf("Enano Sanador", 100, 10, 20);
        MockItem pocion = new MockItem("Poción Grande", 0, 0, 60);
        enano.AddItem(pocion);
        
        MockCharacter objetivo = new MockCharacter("Objetivo", 50, 10, 15);

        enano.Heal(objetivo);

        Assert.That(objetivo.Health, Is.EqualTo(objetivo.MaxHealth));
    }

    [Test]
    public void TryToHealADeadCharacter()
    {
        Dwarf enano = new Dwarf("Enano Sanador", 100, 10, 20);
        MockItem pocion = new MockItem("Poción", 0, 0, 25);
        enano.AddItem(pocion);
        
        MockCharacter objetivo = new MockCharacter("Objetivo", 0, 10, 15);
        objetivo.IsAlive = false;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.Heal(objetivo);

        Assert.That(sw.ToString(), Does.Contain("ERROR"));
        Assert.That(sw.ToString(), Does.Contain("ya esta muerto"));
    }

    [Test]
    public void TryToAddSameTypeItemWhenCharacterAlreadyHasOne()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        Weapon arma1 = new Weapon("Espada", 10, 5, false);
        Weapon arma2 = new Weapon("Hacha", 12, 6, false);

        enano.AddItem(arma1);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.AddItem(arma2);

        Assert.That(enano.Items.Count, Is.EqualTo(1));
        Assert.That(sw.ToString(), Does.Contain("ERROR"));
        Assert.That(sw.ToString(), Does.Contain("Ya tienes un item de tipo"));
    }

    [Test]
    public void TryToAddNonWeaponItemWhenCharacterHasNoItems()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        MockItem item = new MockItem("Escudo", 0, 10, 0);

        enano.AddItem(item);

        Assert.That(enano.Items.Count, Is.EqualTo(1));
        Assert.That(enano.Items[0], Is.EqualTo(item));
        // Verificar que no se duplicaron los valores (solo pasa con Weapon)
        Assert.That(item.Defense, Is.EqualTo(10));
    }

    [Test]
    public void TryToRemoveItemWhenCharacterHasItem()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        MockItem item = new MockItem("Item Prueba", 5, 3, 0);
        enano.AddItem(item);

        enano.RemoveItem(item);

        Assert.IsEmpty(enano.Items);
    }

    [Test]
    public void TryToRemoveItemWhenCharacterDoesntHaveItem()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        MockItem item = new MockItem("Item Inexistente", 5, 3, 0);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        enano.RemoveItem(item);

        Assert.That(sw.ToString(), Does.Contain("ERROR"));
        Assert.That(sw.ToString(), Does.Contain("no tenia"));
    }

    [Test]
    public void VerifyGetTotalDamageWithMultipleItems()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        Weapon arma = new Weapon("Espada", 5, 0, false); // Se duplicará a 10 por ser Weapon
        MockItem escudo = new MockItem("Escudo", 8, 0, 0);
        
        enano.AddItem(arma);
        enano.AddItem(escudo);

        int totalDamage = enano.GetTotalDamage();
        
        Assert.That(totalDamage, Is.EqualTo(38)); // 20 + (5*2) + 8 = 20 + 10 + 8
    }

    [Test]
    public void VerifyGetTotalDefenseWithMultipleItems()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 15.5, 20);
        Weapon arma = new Weapon("Espada", 0, 3, false); // Se duplicará a 6 por ser Weapon
        MockItem escudo = new MockItem("Escudo", 0, 2, 0);
        
        enano.AddItem(arma);
        enano.AddItem(escudo);

        double totalDefense = enano.GetTotalDefense();
        
        Assert.That(totalDefense, Is.EqualTo(23.5)); // 15.5 + (3*2) + 2 = 15.5 + 6 + 2
    }
    
    [Test]
    public void VerifyGetTotalHealWithoutHealingItems()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        MockItem item = new MockItem("Item Sin Curación", 5, 3, 0);
        enano.AddItem(item);

        int totalHeal = enano.GetTotalHeal();
        
        Assert.That(totalHeal, Is.EqualTo(0));
    }

    [Test]
    public void VerifyWeaponBonusIsAppliedOnlyToWeaponType()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        Weapon arma = new Weapon("Espada", 10, 5, false);
        MockItem otroItem = new MockItem("Escudo", 0, 8, 0);

        int armaAttackInicial = arma.Attack;
        int armaDefenseInicial = arma.Defense;
        int otroItemDefenseInicial = otroItem.Defense;

        enano.AddItem(arma);
        enano.AddItem(otroItem);

        // Weapon debe tener bonus x2
        Assert.That(arma.Attack, Is.EqualTo(armaAttackInicial * 2));
        Assert.That(arma.Defense, Is.EqualTo(armaDefenseInicial * 2));
        
        // Otros items no deben tener bonus
        Assert.That(otroItem.Defense, Is.EqualTo(otroItemDefenseInicial));
    }

    [Test]
    public void VerifyMaxHealthDoesntChangeEvenIfADwarfWasDead()
    {
        Dwarf enano = new Dwarf("Enano Prueba", 100, 10, 20);
        Dwarf enano2 = new Dwarf("Enano Prueba 2", 10, 10, 20);

        double expectedMaxHealth = enano2.MaxHealth;
    
        // Capturar la salida de consola para evitar mostrar mensajes durante el test
        using var sw = new StringWriter();
        Console.SetOut(sw);
    
        enano.Attack(enano2);

        Assert.That(enano2.MaxHealth, Is.EqualTo(expectedMaxHealth));
    
        // El using automáticamente restaura la consola cuando se sale del bloque
    }
    
}