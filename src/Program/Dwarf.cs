namespace Program;

public class Dwarf: ICharacter
{
    public string Name { get; set; }
    public double Health { get; set; }
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


    public Dwarf(string thisName, double thisHealth, double thisDefense, int thisDamage)
    {
        this.Name = thisName;
        this.Health = thisHealth;
        this.Defense = thisDefense;
        this.Damage = thisDamage;
        
        this.Items = new List<IItem>();
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

    public void Heal(ICharacter characterHealed)
    {
        throw new NotImplementedException();
    }

    public void AddItem(IItem itemAdded)
    {
        // A los elfos ser muy efectivos con armas cuerpo a cuerpo,
        // se le multipica la defensa y el ataque de dicho item si es de tipo Weapon
        if (this.Items.Contains(itemAdded))
        {
            Console.WriteLine("ERROR " + this.Name + " ya tiene un/a " + itemAdded.Name);
        }
        else
        {
            if (itemAdded is Weapon)
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
}