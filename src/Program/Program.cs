namespace Program;

class Program
{
    static void Main(string[] args)
    {
        Dwarf enano = new Dwarf("Karl", 100, 45, 50);

        Healing potion = new Healing("Pocion Sanadora", 0, true, 30);
        Weapon sword = new Weapon("Espada", 30, 5, false);
        
        enano.AddItem(potion);
        enano.RemoveItem(sword);
        enano.AddItem(sword);
        enano.RemoveItem(potion);
        enano.AddItem(sword);
    }
}