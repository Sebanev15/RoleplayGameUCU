namespace Program;

public class Healing: IItem
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IsMagical { get; set; }
    public int HealValue { get; set; }

    public Healing(string unNombre,int defense, bool isMagical, int healValue)
    {
        this.Name = unNombre;
        this.Attack = 0;
        this.Defense = defense;
        this.IsMagical = isMagical;
        this.HealValue = healValue;
    }
}