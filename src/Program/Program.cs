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

        Spell hechizo = new Spell("bola de fuego", 200, 0, 0);
        SpellBook libro = new SpellBook("necronomicon");
        Console.WriteLine(libro.Attack);
        libro.AddSpell(hechizo);
        Console.WriteLine(libro.Attack);

        Wizard mago = new Wizard("gandalf", 200, 20, 10, libro);
     

    }
}