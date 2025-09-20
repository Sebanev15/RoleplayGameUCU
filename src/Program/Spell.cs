namespace Program;

public class Spell
{
    public string Name { get; set;}
    public int Damage { get; set;}
    public int Defense { get; set; }
    public int Healing { get; set; }

    public Spell(string name, int damage, int defense, int healing)
    {
        this.Name = name;
        this.Damage = damage;
        this.Defense = defense;
        this.Healing = healing;
    }

}