namespace Program;

public class SpellBook:IItem
{
    public string Name { get; set; }
    public List<Spell>Spells {get;}

    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IsMagical { get; set; }
    public bool IsHealing { get; set; }

    public SpellBook(string name)
    {
        this.Name = name;
        this.Spells = new List<Spell>();
        this.Attack = this.GetSpellDamage();
        this.Defense = this.getSpellDefense();
        this.IsMagical = true;
        
    }
    
    public void AddSpell(Spell spellToAdd) 
    {
        if (this.Spells.Contains(spellToAdd))
        {
            Console.WriteLine($"El hechizo {spellToAdd} ya esta en el libro {this.Name}");
        }
        else
        {
            
            this.Spells.Add(spellToAdd);  
            this.Attack = this.GetSpellDamage();
            this.Defense = this.getSpellDefense();
        }
        
    }
    
    public void RemoveSpell(Spell spellToRemove)
    {
        if (this.Spells.Contains(spellToRemove))
        {
            this.Spells.Remove(spellToRemove);   
            this.Attack = this.GetSpellDamage();
            this.Defense = this.getSpellDefense();
        }
        else
        {
            Console.WriteLine($"El hechizo {spellToRemove} no se encuentra en el libro {this.Name}");
        }
        
    }

    private int  GetSpellDamage()
    {
        int totalDamage = 0;
        if (Spells.Count>0)
        {
            
            foreach (Spell spell in Spells)
            {
                totalDamage += spell.Damage;
            }

            
        }
        return totalDamage;
    }

    private int getSpellDefense()
    {
        
        int totalDefense = 0;
        if (Spells.Count>0)
        {
            
            foreach (Spell spell in Spells)
            {
                totalDefense += spell.Defense;
            }

            
        }
        return totalDefense;

    }

}