namespace Program;

public interface ICharacter
{
    public string Name { get; set; }
    public double Health { get; set; }
    public List<IItem> Items { get; }
    public double Defense { get; set; }
    public int Damage { get; set; }

    public bool IsAlive { get; set; }

    public void Attack(ICharacter characterAttacked);

    public void Heal(ICharacter characterHealed);

    public void AddItem(IItem itemAdded);

    public void RemoveItem(IItem itemRemoved);

    public int GetTotalDamage();

    public double GetTotalDefense();
}