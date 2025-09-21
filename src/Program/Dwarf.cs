namespace Program;

public class Dwarf: ICharacter
{
    public string Name { get; set; }
    public double Health { get; set; }
    public double MaxHealth { get;  }
    public List<IItem> Items { get; }
    public double Defense { get; set; }
    public int Damage { get; set; }
    private bool isAlive = true;
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }

        set
        {
            if (isAlive)
            {
                isAlive = value;
            }
        }
    }


    public Dwarf(string thisName, double thisHealth, double thisDefense, int thisDamage)
    {
        this.Name = thisName;
        this.Health = thisHealth;
        this.MaxHealth = thisHealth;
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
        if (characterHealed.IsAlive)
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
        else
        {
            Console.WriteLine("ERROR: " + characterHealed.Name + " ya esta muerto, por lo que no se puede curar");
        }
    }

    public void AddItem(IItem itemAdded)
    {
        //Verifico que el personaje no tenga un elemento del tipo que se intenta añadir
        foreach (IItem item in this.Items)
        {
            if (item.GetType() == itemAdded.GetType())
            {
                //Si llega a tener un item del mismo tipo se muestra un mensaje de error y se hace un return para salir del metodo
                Console.WriteLine("ERROR: Ya tienes un item de tipo " + item.GetType() + " para poder añadir un/a " + itemAdded.Name + ", primero tienes que eliminar al item [" + item.Name + "] ");
                
                return;
            }
        }
        // A los enanos ser muy efectivos con armas cuerpo a cuerpo,
        // se le multipica la defensa y el ataque de dicho item si es de tipo Weapon
        if (itemAdded is Weapon)
        {
            itemAdded.Attack *= 2;
            itemAdded.Defense *= 2;
        }
        this.Items.Add(itemAdded);
        
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
}