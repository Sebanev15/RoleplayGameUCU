namespace Program;

class Program
{
    static void Main(string[] args)
    {
        Dwarf enano = new Dwarf("Enanito Gonzalez", 100, 50, 50);

        Weapon archa = new Weapon("archa", 30, 30, true);
        
        enano.RemoveItem(archa);
        enano.AddItem(archa);
        enano.AddItem(archa);
    }
}