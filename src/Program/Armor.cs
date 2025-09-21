namespace Program;

public class Armor: IItem
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IsMagical { get; set; }
    public int HealValue { get; set; }
    public Armor(string unNombre, int attack, int defense, bool isMagical)
    {
        this.Name = unNombre;
        this.Attack = attack;
        this.Defense = defense;
        this.IsMagical = isMagical;
        this.IsHealing = false;
    }
}