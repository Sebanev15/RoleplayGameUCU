namespace Program;

public class Wizard: ICharacter
{

    public string Name { get; set; }
    public double Health { get; set; }
    
    public List<IItem> Items { get; set; }
    
    public double Defense { get; set; }
    
    public int Damage { get; set; }
    
    public bool IsAlive { get; set; }
    public SpellBook SpellBook { get; set; }

    public Wizard(string name, double health, double defense, int damage, SpellBook spellBook)
    {
        this.Name = name;
        this.Health = health;
        this.Defense = defense;
        this.Damage = damage;
        this.IsAlive = true;
        this.Items = new List<IItem>();
        

    }

    public void AddItem(IItem itemToAdd)
    {
        if (this.Items.Contains(itemToAdd))
        {
         Console.WriteLine($"Error el arma {itemToAdd.Name} ya estaba equipada");
        }
        else
        {
            if (itemToAdd is SpellBook || (itemToAdd is Weapon && itemToAdd.IsMagical == true))
            {
                itemToAdd.Attack *= 2;
                itemToAdd.Defense *= 2;
            }

            this.Items.Add(itemToAdd);
        }
    }

    public void RemoveItem(IItem itemToRemove)
    {
        if (Items.Contains(itemToRemove))
        {
            if (itemToRemove is SpellBook || (itemToRemove is Weapon && itemToRemove.IsMagical==true))
            {
                itemToRemove.Attack /= 2;
                itemToRemove.Defense /= 2;
            }
            Items.Remove(itemToRemove);
        }
        else
        {
            Console.WriteLine($"El {itemToRemove.Name} item no esta en el inventario de {this.Name}");
        }
    }

    public void Attack(ICharacter characterAttacked)
    {
        int totalDamage = this.GetTotalDamage();
        characterAttacked.Health -= totalDamage-characterAttacked.GetTotalDefense()*0.75;

    }

    public void Heal(ICharacter characterHealed)
    {
        
        
    }

    public int GetTotalDamage()
    {
        int totalDamage = 0;
        foreach (IItem item in Items)
        {
            totalDamage += item.Attack;

        }

        return totalDamage; 
    }
    
    public double GetTotalDefense()
    {
        double totalDefense = this.Defense;
        foreach (IItem item in Items)
        {
            totalDefense += item.Defense;
        }

        return totalDefense;
    }

}