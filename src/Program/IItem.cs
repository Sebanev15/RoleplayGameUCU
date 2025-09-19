namespace Program;

public interface IItem
{
    public string Name;
    public int Attack;
    public int Defense;
    public bool IsMagical;

    public void AddToCharacter(ICharacter);
    public void RemoveToCharacter(ICharacter);
}