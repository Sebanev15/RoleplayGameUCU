namespace Program;

public class Elve : ICharacter
{
    public string Name { get; set; }
    public double Health { get; set; }
    public double MaxHealth { get; }
    public List<IItem> Items { get; }
    public double Defense { get; set; }
    public int Damage { get; set; }
    public bool IsAlive
    {
        get
        {
            return IsAlive;
        }

        set
        {
            if (IsAlive)
            {
                IsAlive = value;
            }
        }
    }
    public Elve(string thisName, double thisHealth, double thisDefense, int thisDamage)
    {
        this.Name = thisName;
        this.Health = thisHealth;
        this.MaxHealth = thisHealth;
        this.Defense = thisDefense;
        this.Damage = thisDamage;
    }
    
    public void Attack(ICharacter characterAttacked)
    {
        if (characterAttacked.IsAlive)
        {
            int totalDamage = this.GetTotalDamage();

            double totalDefense = characterAttacked.GetTotalDefense();

            double totalDamageCaused = totalDamage - (totalDefense*0.75);

            if (totalDamageCaused < 0)
            {
                totalDamageCaused = 0;
            }

            characterAttacked.Health -= totalDamageCaused;
        
            Console.WriteLine("Se ha infligido un total de " + totalDamageCaused + " de daño a " + characterAttacked.Name);
            if (characterAttacked.Health <= 0)
            {
                Console.WriteLine(characterAttacked.Name + " ha sido asesinado");
                characterAttacked.IsAlive = false;
            }    
        }
        else
        {
            Console.WriteLine("ERROR " + characterAttacked.Name + " ya esta muerto, por lo que no se lo puede atacar");
        }
        
    }
    
    public void AddItem(IItem itemAdded)
    {
        if (this.Items.Contains(itemAdded))
        {
            Console.WriteLine("ERROR " + this.Name + " ya tiene un/a " + itemAdded.Name);
        }
        else
        {
            //Al ser mágicos los elfos, cada item cuyo atributo IsMagical sea true va a tener su ataque y defensa aumentado al ser añadido a un Elve
            if (itemAdded.IsMagical == true)
            {
                itemAdded.Attack *= 2;
                itemAdded.Defense *= 2;
            }
            this.Items.Add(itemAdded);    
        }
    }
    
    public void RemoveItem(IItem itemRemoved)
    {
        if(this.Items.Contains(itemRemoved))
        {
            this.Items.Remove(itemRemoved);
        }
        else
        {
            Console.WriteLine("ERROR " + this.Name + " no tenia un/a " + itemRemoved.Name);
        }
    }
    public int GetTotalDamage()
    {
        int totalDamage = this.Damage;
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

    public int GetTotalHeal()
    {
        int totalHeal=0;
        foreach (IItem item in Items)
        {
            totalHeal += item.HealValue;
        }

        return totalHeal;
    }

    public void Heal(ICharacter characterHealed)
    {
        int totalHeal = this.GetTotalHeal();
        if ((characterHealed.Health + totalHeal) >= characterHealed.MaxHealth)
        {
            characterHealed.Health = characterHealed.MaxHealth;
        }
        else
        {
            characterHealed.Health += totalHeal;
        }
    }
}