namespace Program;

public interface IItem
{
    public string Name { get; set; }
    public int Attack{ get; set; }
    public int Defense{ get; set; }
    public bool IsMagical{ get; set; }
    public bool IsHealing { get; set; }
}