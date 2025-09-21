namespace Program;

public class Wizard: ICharacter
{

    public string Name { get; set; }
    public double Health { get; set; }
    public double MaxHealth { get; set; }
    public List<IItem> Items { get; set; }
    
    public double Defense { get; set; }
    
    public int Damage { get; set; }

    private bool isAlive = true;
    
    public bool IsAlive {  get
        {
            return isAlive;
        }

        set
        {
            if (isAlive)
            {
                isAlive = value;
            }
        }}
    
    

    public Wizard(string name, double health, double defense, int damage)
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

        foreach (IItem item in Items)
        {
            if (item.GetType()==itemToAdd.GetType())
            {
                Console.WriteLine($"ya tienes un item de este tipo");
            }
            return;

        }
            if (itemToAdd is SpellBook || (itemToAdd is Weapon && itemToAdd.IsMagical))
            {
                itemToAdd.Attack *= 2;
                itemToAdd.Defense *= 2;
                itemToAdd.HealValue *= 2;
            }

            this.Items.Add(itemToAdd);
    }

    public void RemoveItem(IItem itemToRemove)
    {
        if (Items.Contains(itemToRemove))
        {
            if (itemToRemove is SpellBook || (itemToRemove is Weapon && itemToRemove.IsMagical))
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
        if (characterHealed.IsAlive)
        {
            double healing = GetTotalHeal() +characterHealed.Health;
            
           
            if (healing>=characterHealed.MaxHealth)
            {
                characterHealed.Health = characterHealed.MaxHealth;
            }
            else
            {
                characterHealed.Health += healing;
            }
            
        }
        else
        {
            Console.WriteLine($"{characterHealed} ya se encuentra muerto, no hay nada mas que hacer por él");
            
        }

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
    
    public int GetTotalHeal()
    {
        int totalHealing = 0;
        foreach (IItem item in Items)
        {
            
                totalHealing += item.HealValue;
            
            
        }

        return totalHealing;
    }

}