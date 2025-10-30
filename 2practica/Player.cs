class Player : Entity, ILevelable
{
    public Player(string name, double maxHealth) : base(name, maxHealth)
    {
        Level = 1;
        Experience = 0;
    }

    public int Level { get; private set; }
    public double Experience { get; private set; }
    public int CD { get; set; } = 16;
    public int EvadeChance { get; } = 30;

    public override double DoSomeDmg(double modifier)
    {
        return _baseDamage * modifier * (1 + (Level - 1) * 0.15);
    }

    public override void TakeDamage(double damage)
    {
        Health = Math.Max(0, Math.Min(MaxHealth, Health - damage));
    }

    public void Heal(double amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
    }

    public void GainExperience(double exp)
    {
        Experience += exp;
        Console.WriteLine($"{Name} получил {exp:F2} опыта. Текущий опыт: {Experience:F2}/{Level * 100:F2}");
        if (Experience >= Level * 100)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        Experience = 0;
        _baseDamage *= 1.2;
        _maxHealth *= 1.1;
        Health = MaxHealth;
        Console.WriteLine($" {Name} повысил уровень до {Level}! Урон +20%, Здоровье +10%");
        Console.WriteLine($"{Name} теперь имеет {MaxHealth:F2} HP и урон {_baseDamage:F2}");
        
        Thread.Sleep(2000);
    }
}