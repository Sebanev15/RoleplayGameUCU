namespace Program;

public class Shield: IItem
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IsMagical { get; set; }
    public bool IsHealing { get; set; }
    public Shield(string unNombre, int attack, int defense, bool isMagical, bool isHealing)
    {
        this.Name = unNombre;
        this.Attack = attack;
        this.Defense = defense;
        this.IsMagical = isMagical;
        this.IsHealing = isHealing;
    }
}
