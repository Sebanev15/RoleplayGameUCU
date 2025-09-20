namespace Program;

public interface ICharacter
{
    public string Name { get; set; }
    public float Health { get; set; }
    public List<IItem> Items { get; }
    public int Defense { get; set; }
    public int Damage { get; set; }

    public void Attack(ICharacter characterAttacked);

    public void Heal(ICharacter characterHealed);

    public void AddItem(IItem itemAdded);

    public void RemoveItem(IItem itemRemoved);

    public int GetTotalDamage();

    public int GetTotalDefense();
}