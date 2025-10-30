class Enemy : Entity, ILevelable
{
    public Enemy(string name, double maxHealth) : base(name, maxHealth)
    {
        Level = 1;
        Experience = 0;
    }

    public int Level { get; private set; }
    public double Experience { get; private set; }
    public int CD { get; set; } = 10;
    public int EvadeChance { get; set; } = 10;

    public override double DoSomeDmg(double modifier)
    {
        return _baseDamage * modifier * (1 + (Level - 1) * 0.1);
    }

    public void GainExperience(double exp)
    {
        Experience += exp;
        if (Experience >= Level * 50)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        Experience = 0;
        _baseDamage *= 1.1;
        Console.WriteLine($"{Name} повысил уровень до {Level}! Урон увеличен.");
    }
}